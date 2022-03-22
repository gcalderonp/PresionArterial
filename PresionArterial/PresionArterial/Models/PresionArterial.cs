using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PresionArterial.Models
{
    public class PresionArterial
    {

        private int _bloodPressure_id;

        [PrimaryKey, AutoIncrement]
        public int bloodPressure_id
        {
            get { return _bloodPressure_id; }
            set { _bloodPressure_id = value; }
        }

        private int _User_id;

        [Indexed]
        public int User_id
        {
            get { return _User_id; }
            set { _User_id = value; }
        }

        private DateTime _DateTime;

        public DateTime Date
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        private int _Diastole;

        public int Diastole
        {
            get { return _Diastole; }
            set { _Diastole = value; }
        }

        private int _Systole;

        public int Systole
        {
            get { return _Systole; }
            set { _Systole = value; }
        }

        private string _StatusPresion;

        public string StatusPresion
        {
            get { return _StatusPresion; }
            set { _StatusPresion = value; }
        }


    }
}
