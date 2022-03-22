using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PresionArterial.Models
{
    public class Usuario
    {

        private int _id;

        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private DateTime _Date_of_birth;

        public DateTime Date_of_birth
        {
            get { return _Date_of_birth; }
            set { _Date_of_birth = value; }
        }


    }
}
