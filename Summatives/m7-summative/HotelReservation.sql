SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='HotelReservation')
		drop database HotelReservation
go

if exists (select * from sysobjects where id = object_id('dbo.BillingDetails_RoomBilling'))
	alter table BillingDetails drop constraint BillingDetails_RoomBilling;
GO
if exists (select * from sysobjects where id = object_id('dbo.Customer_Reservation'))
	ALTER TABLE Customer DROP CONSTRAINT Customer_Reservation;
GO
if exists (select * from sysobjects where id = object_id('dbo.Reservation_Customer'))
	ALTER TABLE Reservation DROP CONSTRAINT Reservation_Customer;
GO
if exists (select * from sysobjects where id = object_id('dbo.Reservation_Details_Reservation'))
	ALTER TABLE ReservedRoomDetails DROP CONSTRAINT Reservation_Details_Reservation;
GO
if exists (select * from sysobjects where id = object_id('dbo.Reservation_Details_Room'))
	ALTER TABLE ReservedRoomDetails DROP CONSTRAINT Reservation_Details_Room;
GO
if exists (select * from sysobjects where id = object_id('dbo.Reservation_Promotions'))
	ALTER TABLE Reservation DROP CONSTRAINT Reservation_Promotions;
GO
if exists (select * from sysobjects where id = object_id('dbo.Room_Billing_Reservation'))
	ALTER TABLE RoomBilling DROP CONSTRAINT Room_Billing_Reservation;
GO
if exists (select * from sysobjects where id = object_id('dbo.Room_Rates_Room_Type'))
	ALTER TABLE RoomRate DROP CONSTRAINT Room_Rates_Room_Type;
GO
if exists (select * from sysobjects where id = object_id('dbo.Room_Room_Type'))
	ALTER TABLE Room DROP CONSTRAINT Room_Room_Type;
GO
if exists (select * from sysobjects where id = object_id('dbo.BillingDetails') and sysstat & 0xf = 3)
	drop table "dbo"."BillingDetails"
GO
if exists (select * from sysobjects where id = object_id('dbo.Customer') and sysstat & 0xf = 3)
	drop table "dbo"."Customer"
GO
if exists (select * from sysobjects where id = object_id('dbo.Promotion') and sysstat & 0xf = 3)
	drop table "dbo"."Promotion"
GO
if exists (select * from sysobjects where id = object_id('dbo.Reservation') and sysstat & 0xf = 3)
	drop table "dbo"."Reservation"
GO
if exists (select * from sysobjects where id = object_id('dbo.ReservedRoomDetails') and sysstat & 0xf = 3)
	drop table "dbo"."ReservedRoomDetails"
GO
if exists (select * from sysobjects where id = object_id('dbo.Room') and sysstat & 0xf = 3)
	drop table "dbo"."Room"
GO
if exists (select * from sysobjects where id = object_id('dbo.RoomBilling') and sysstat & 0xf = 3)
	drop table "dbo"."RoomBilling"
GO
if exists (select * from sysobjects where id = object_id('dbo.RoomRate') and sysstat & 0xf = 3)
	drop table "dbo"."RoomRate"
GO
if exists (select * from sysobjects where id = object_id('dbo.RoomType') and sysstat & 0xf = 3)
	drop table "dbo"."RoomType"
GO

USE Master;
GO

CREATE DATABASE HotelReservation;
GO

USE HotelReservation;
GO
-- tables
-- Table: BillingDetails
CREATE TABLE BillingDetails (
    BillingDetailsId int  NOT NULL IDENTITY(1, 1),
    BillingId int  NOT NULL,
    Description varchar(30)  NOT NULL,
    Amount DECIMAL(9,2)  NOT NULL,
    CONSTRAINT BillingDetails_pk PRIMARY KEY  (BillingDetailsId,BillingId)
);

-- Table: Customer
CREATE TABLE Customer (
    CustomerId int  NOT NULL IDENTITY(1, 1),
    Name varchar(30)  NOT NULL,
    Age tinyint  NOT NULL,
    Phone char(10)   NULL,
    Email varchar(40)   NULL,
    ReservationId int  NULL,
    CONSTRAINT Customer_pk PRIMARY KEY  (CustomerId)
);

-- Table: Promotion
CREATE TABLE Promotion (
    PromotionId int  NOT NULL IDENTITY(1, 1),
    PromoTypeDescription varchar(30)  NOT NULL,
    StartDate date  NOT NULL,
    EndDate date  NOT NULL,
    DiscountType varchar(10)  NOT NULL,
    DiscountValue DECIMAL(5,2)  NOT NULL,
    CONSTRAINT Promotion_pk PRIMARY KEY  (PromotionId)
);

-- Table: Reservation
CREATE TABLE Reservation (
    ReservationId int  NOT NULL IDENTITY(1, 1),
    CustomerId int  NOT NULL,
    PromotionsId int  NOT NULL,
    CONSTRAINT Reservation_pk PRIMARY KEY  (ReservationId)
);

-- Table: ReservedRoomDetails
CREATE TABLE ReservedRoomDetails (
    RoomId int  NOT NULL,
    DateIn date  NOT NULL,
    DateOut date  NOT NULL,
    ReservationId int  NOT NULL,
    CONSTRAINT ReservedRoomDetails_pk PRIMARY KEY  (RoomId,DateIn,DateOut)
);

-- Table: Room
CREATE TABLE Room (
    RoomId int  NOT NULL IDENTITY(1, 1),
    Number varchar(10)  NOT NULL,
    Name varchar(40)  NOT NULL,
    Status varchar(10)  NOT NULL,
    RoomTypeId int  NOT NULL,
    Floor int  NOT NULL,
    CONSTRAINT Room_pk PRIMARY KEY  (RoomId)
);

-- Table: RoomBilling
CREATE TABLE RoomBilling (
    BillingId int  NOT NULL IDENTITY(1, 1),
    ReservationId int  NOT NULL,
    BillingDate date  NOT NULL,
    CONSTRAINT RoomBilling_pk PRIMARY KEY  (BillingId)
);

-- Table: RoomRate
CREATE TABLE RoomRate (
    RoomTypeId int  NOT NULL,
    StartDate date  NOT NULL,
    EndDate date  NOT NULL,
    RateTypeDescription varchar(30)  NOT NULL,
    RoomCost DECIMAL(6,2)  NOT NULL,
    ServiceCharges DECIMAL(4,2)  NOT NULL,
    MovieCharges DECIMAL(4,2)  NOT NULL,
    OtherCharges DECIMAL(4,2)  NOT NULL,
    CONSTRAINT RoomRate_pk PRIMARY KEY  (RoomTypeId,StartDate,EndDate)
);

-- Table: RoomType
CREATE TABLE RoomType (
    RoomTypeId int  NOT NULL,
    Description varchar(20)  NOT NULL,
    OccupancyLimit tinyint  NOT NULL,
    Amenities varchar(80)  NOT NULL,
    CONSTRAINT RoomType_pk PRIMARY KEY  (RoomTypeId)
);

-- foreign keys
-- Reference: BillingDetails_RoomBilling (table: BillingDetails)
ALTER TABLE BillingDetails ADD CONSTRAINT BillingDetails_RoomBilling
    FOREIGN KEY (BillingId)
    REFERENCES RoomBilling (BillingId);

-- Reference: Customer_Reservation (table: Customer)
ALTER TABLE Customer ADD CONSTRAINT Customer_Reservation
    FOREIGN KEY (ReservationId)
    REFERENCES Reservation (ReservationId);

-- Reference: Reservation_Customer (table: Reservation)
ALTER TABLE Reservation ADD CONSTRAINT Reservation_Customer
    FOREIGN KEY (CustomerId)
    REFERENCES Customer (CustomerId);

-- Reference: Reservation_Details_Reservation (table: ReservedRoomDetails)
ALTER TABLE ReservedRoomDetails ADD CONSTRAINT Reservation_Details_Reservation
    FOREIGN KEY (ReservationId)
    REFERENCES Reservation (ReservationId);

-- Reference: Reservation_Details_Room (table: ReservedRoomDetails)
ALTER TABLE ReservedRoomDetails ADD CONSTRAINT Reservation_Details_Room
    FOREIGN KEY (RoomId)
    REFERENCES Room (RoomId);

-- Reference: Reservation_Promotions (table: Reservation)
ALTER TABLE Reservation ADD CONSTRAINT Reservation_Promotions
    FOREIGN KEY (PromotionsId)
    REFERENCES Promotion (PromotionId);

-- Reference: Room_Billing_Reservation (table: RoomBilling)
ALTER TABLE RoomBilling ADD CONSTRAINT Room_Billing_Reservation
    FOREIGN KEY (ReservationId)
    REFERENCES Reservation (ReservationId);

-- Reference: Room_Rates_Room_Type (table: RoomRate)
ALTER TABLE RoomRate ADD CONSTRAINT Room_Rates_Room_Type
    FOREIGN KEY (RoomTypeId)
    REFERENCES RoomType (RoomTypeId);

-- Reference: Room_Room_Type (table: Room)
ALTER TABLE Room ADD CONSTRAINT Room_Room_Type
    FOREIGN KEY (RoomTypeId)
    REFERENCES RoomType (RoomTypeId);

-- End of file.

INSERT INTO RoomType (RoomTypeId, Description, OccupancyLimit, Amenities) VALUES(1,'Single',1,'Bathroom & Bed');
INSERT INTO RoomType (RoomTypeId, Description, OccupancyLimit, Amenities) VALUES(2, 'Double',2,'Bathroom, Bed & Fridge');
INSERT INTO RoomType (RoomTypeId, Description, OccupancyLimit, Amenities) VALUES(3, 'King',3,'Bathroom, King Bed, Fridge and Spa');

INSERT INTO ROOM (Number,Name,Status,RoomTypeId, Floor) VALUES('101','Saturn','Open',1,1);
INSERT INTO ROOM (Number,Name,Status,RoomTypeId, Floor) VALUES('201','Pluto','Booked',2,2);
INSERT INTO ROOM (Number,Name,Status,RoomTypeId, Floor) VALUES('301','Uranus','Open'   ,3,3);
INSERT INTO ROOM (Number,Name,Status,RoomTypeId, Floor) VALUES('407','Jupiter','Open'   ,3,4);

INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(1,'2000-01-01','2019-12-23','Regular',100.00,10.00,5.00,5.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(1,'2019-12-24','2019-12-25','Christmas',90.00,9.00,4.00,4.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(1,'2019-12-26','9999-12-31','Regular',100.00,10.00,5.00,5.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(2,'2000-01-01','2019-12-23','Regular',200.00,10.00,5.00,5.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(2,'2019-12-24','2019-12-25','Christmas',190.00,9.00,4.00,4.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(2,'2019-12-26','9999-12-31','Regular',200.00,10.00,5.00,5.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(3,'2000-01-01','2019-12-23','Regular',300.00,10.00,5.00,5.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(3,'2019-12-24','2019-12-25','Christmas',290.00,9.00,4.00,4.00);
INSERT INTO RoomRate (RoomTypeId,StartDate,EndDate,RateTypeDescription,RoomCost,ServiceCharges,MovieCharges,OtherCharges) VALUES(3,'2019-12-26','9999-12-31','Regular',300.00,10.00,5.00,5.00);

INSERT INTO Promotion (PromoTypeDescription,StartDate,EndDate,DiscountType,DiscountValue) VALUES('Thanks Giving','2019-11-28','2019-11-28','Flatamount',25.00);
INSERT INTO Promotion (PromoTypeDescription,StartDate,EndDate,DiscountType,DiscountValue) VALUES('Christmas','2019-12-25','2019-12-25','Percentage',10.00);

INSERT INTO CUSTOMER (Name,Age,Phone,Email) VALUES('Reservation1Name1',35,'1112223333','abc@gmail.com');
INSERT INTO CUSTOMER (Name,Age,Phone,Email) VALUES('Reservation1Name2',30,'1112223333','abc@gmail.com');
INSERT INTO CUSTOMER (Name,Age,Phone,Email) VALUES('Reservation2Name1',40,'2222223333','bbc@gmail.com');
INSERT INTO CUSTOMER (Name,Age,Phone,Email) VALUES('Reservation2Name2',30,'','');

INSERT INTO Reservation (CustomerId, PromotionsId) VALUES(1,2);
INSERT INTO Reservation (CustomerId, PromotionsId) VALUES(4,2);

UPDATE CUSTOMER SET ReservationId = 1 WHERE CustomerId IN (1,2);
UPDATE CUSTOMER SET ReservationId = 2 WHERE CustomerId IN (3,4);

INSERT INTO ReservedRoomDetails VALUES(2,'2019-12-24','2019-12-26',1);
INSERT INTO ReservedRoomDetails VALUES(4,'2019-12-20','2019-12-30',2);

INSERT INTO RoomBilling (ReservationId, BillingDate) VALUES(1,'2019-12-26');
INSERT INTO RoomBilling (ReservationId, BillingDate) VALUES(2,'2019-12-30');

INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(1,'Room Charges 2*190',380.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(1,'Service Charges 2*9',18.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(1,'Movie Charges 2*4',8.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(1,'Other Charges 2*4',8.00);

INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(2,'Room Charges 2*290+8*300',2980.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(2,'Service Charges 2*9+8*10',98.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(2,'Movie Charges 2*4+8*5',48.00);
INSERT INTO BillingDetails (BillingId, Description, Amount) VALUES(2,'Other Charges 2*4+8*5',48.00);