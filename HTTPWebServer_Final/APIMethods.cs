using System;

namespace HTTPWebServer_Final
{
    class APIMethods
    {
        public static string Year(string year)
        {
            int id;
            bool result = Int32.TryParse(year,out id);

            if (result == false)
                return "invalid input";
            if (id % 100 == 0)
                id = id / 100;

            if (id % 4 == 0)
                return "Leap Year";
            else
                return "Not a Leap Year!";
        }

        public static string Hello(string name)
        {
            return ("Hi " + name);
        }
    }
}


