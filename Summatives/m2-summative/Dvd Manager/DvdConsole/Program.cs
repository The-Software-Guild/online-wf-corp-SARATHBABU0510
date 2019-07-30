using DvdManager.Controllers;

namespace DvdConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DvdController dvdcontrol = new DvdController();
            dvdcontrol.Run();
        }
    }
}
