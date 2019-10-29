$(document).ready(function () {

    //Initial State - Load items 
    loadItems();
    
    //Click handler to get item #
    $('.square').click(function() {
        selectItem($(this).attr('id'));
    });
    
    //Button click handler to get totals
    $('#add-dollar, #add-quarter, #add-dime, #add-nickel').click(function () {       
        deriveTotalamount($(this).attr('id'));
    });
    
    //Button click handler to make purchase
    $('#makepurchase').click(function() {
        makePurchase(parseFloat($('#total-in').val()).toFixed(2), $('#item').val());
    });
    
    //Button click handler to change return: Abandon a transaction and clear the input fields.
    $('#changereturn').click(function() {
        changeReturn();
    });
    
}); 

function selectItem(input) {
        $('#item').val('');
        var itemtableId = $("#" + input).find('[id*="item"]').attr('id');        
        var itemtablefirstrowId = "#" + itemtableId + " tr:first";       
        var itemId = $(itemtablefirstrowId).text();
        $('#item').val(itemId);        
}
    
function deriveTotalamount(input) {    
       var addbutton = input;
       $('#total-in').prop("readonly", false);
       var currenttotal = parseFloat($('#total-in').val()) || 0.00;
       var finaltotal = currenttotal;
       switch (addbutton) {
           case "add-dollar":
                finaltotal += 1.00;          
                break;
           case "add-quarter":
                finaltotal += 0.25;          
                break;
           case "add-dime":
                finaltotal += 0.10;          
                break;
           case "add-nickel":
                finaltotal += 0.05;          
                break;
           default:
                finaltotal += 0.00;          
       }
       $('#total-in').val(parseFloat(finaltotal).toFixed(2));
       $('#total-in').prop("readonly", true);
} 
  
function loadItems() {
    // we need to clear the previous content so we don't append to it
   clearItemTables();

    var itemtable = [];
    // grab the the tbody element that will hold the rows of contact information
    for (var loopCounter = 1;loopCounter < 10;loopCounter++) {
        itemtable[loopCounter] = $('#item' + loopCounter);
    }
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function (data, status) {
            $.each(data, function (index, item) {
                var id = item.id;
                var name = item.name;
                var price = item.price;
                var quantity = item.quantity;
                var dollar = '$';
                var Quantity_left_text = 'Quantity Left: ';

                var row = '<tr align="left"><td>' + id + '</td></tr>';
                    row += '<tr><td>' + name + '</td></tr>';
                    row += '<tr><td>' + dollar + parseFloat(price).toFixed(2) + '</td></tr>';
                    row += '<tr><td>' + Quantity_left_text + quantity + '</td></tr>';
                itemtable[index + 1].append(row);
                row = '';
            });
        },
        error: function() {
            $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service.  Please try again later.'));
            $('#bodycontent, #endinghrule').hide();
        }
    });
}

function clearItemTables() {
    for (var loopCounter = 1;loopCounter < 10;loopCounter++) {
        $('#item' + loopCounter).empty();
    }
}

function makePurchase(money, item) {
    
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/money/' + money + '/item/' + item,
        success: function(data, status) {
              $('#messages').prop("readonly", false);
              $('#messages').val('Thank You!!!');
              $('#messages').prop("readonly", true);
              $('#change').prop("readonly", false);
              var balance = '';
              if(data.quarters > 0) {
                    balance += data.quarters + ' Quarter(s) ';
              }
              if(data.dimes > 0) {
                    balance += data.dimes + ' Dime(s) ';
              }
              if(data.nickels > 0) {
                    balance += data.nickels + ' Nickel(s) ';
              }
              if(data.pennies > 0) {
                    balance += data.pennies + ' Penny(s) ';
              }
              $('#change').val(balance);
              $('#change').prop("readonly", true);
          },
        error: function(xhr, textStatus, errorThrown) {            
            if((errorThrown == "Unprocessable Entity") && (xhr.status == "422")) {            
                $('#messages').val((jQuery.parseJSON(xhr.responseText)).message);
            }
            else {
                $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service.  Please try again later.'));
                $('#bodycontent, #endinghrule').hide();
            }            
        }
    });
    
   //Load the current data;
   loadItems();
}

function changeReturn() {              
        var currenttotal = 0.00;
        if ($('#messages').val() != 'Thank You!!!') {
            currenttotal = parseFloat($('#total-in').val()).toFixed(2) || 0.00;
        }
        var quarter = 0.25;
        var dime    = 0.10;
        var nickel  = 0.05;
        var penny   = 0.01;
        var quarters = '', dimes = '', nickels = '', pennies = '', balance = '';
        var quarters = parseInt(currenttotal / quarter);
        currenttotal = parseFloat(currenttotal % quarter).toFixed(2);
        var dimes = parseInt(currenttotal / dime);
        currenttotal = parseFloat(currenttotal % dime).toFixed(2);
        var nickels = parseInt(currenttotal / nickel);
        currenttotal = parseFloat(currenttotal % nickel).toFixed(2);
        var pennies = parseInt(currenttotal / penny);       
        if(quarters > 0) {
            balance += quarters + ' Quarter(s) ';
        }
        if(dimes > 0) {
            balance += dimes + ' Dime(s) ';
        }
        if(nickels > 0) {
            balance += nickels + ' Nickel(s) ';
        }
        if(pennies > 0) {
            balance += pennies + ' Penny(s) ';
        }
       $('#total-in, #messages, #change').prop("readonly", false);       
       $('#total-in, #messages, #item').val('');       
       $('#change').val(balance);
       $('#total-in, #messages, #change').prop("readonly", true);
       //Load the current data;
       loadItems();
}

