using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGardenConsoleVSU
{
    //use this method for weather or other global things.
    class Singleton
    {
        private Singleton()
        {

        }

        //private static variable of Singleton type.
        private static Singleton objSingle = new Singleton();

        //declare a public static property that returns the
        //Singleton object as we can not call the private
        //constructor from outside the class.
        public static Singleton ObjSingle
        {
            get { return Singleton.objSingle; }
            set { Singleton.objSingle = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
