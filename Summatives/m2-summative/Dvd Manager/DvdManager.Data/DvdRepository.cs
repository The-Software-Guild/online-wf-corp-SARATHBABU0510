using System.Collections.Generic;
using DvdManager.Models;

namespace DvdManager.Data
{
    public class DvdRepository
    {
        private static List<Dvd> _dvdlist = new List<Dvd>();
        
        public Dvd Create(Dvd dvd)
        {
            _dvdlist.Add(dvd);
            return dvd;
        }

        public List<Dvd> ReadAll()
        {
            return _dvdlist;
        }

        public  Dvd ReadById(int id)
        {
            Dvd dvd = _dvdlist.Find(dvdid => dvdid.Id == id);
            return dvd;
        }
        public void Update(int id, Dvd dvd)
        {            
            if (exists(id))
            {
                id = _dvdlist.FindIndex(dvdid => dvdid.Id == id);
                _dvdlist[id] = dvd;
            }
        }

        public void Delete(int id)
        {
            if (exists(id))
            {
                Dvd DVD = _dvdlist.Find(dvdid => dvdid.Id == id);
                _dvdlist.Remove(DVD);
            }
        }

        public bool exists(int id)
        {
            bool exists = _dvdlist.Exists(dvdid => dvdid.Id == id);
            if (exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
