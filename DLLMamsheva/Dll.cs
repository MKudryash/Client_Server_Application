using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLMamsheva
{
    public class Dll
    {
        public int AvageAge( List<DateTime> dateTimes)
        {
            DateTime now = DateTime.Today;
            int count=0;
            int age = 0;
            foreach (DateTime dT in dateTimes)
            {
                age += now.Year - dT.Year;
                if (dT > now.AddYears(-age)) age--;
                count++;
            }
            age = age / count;
            return age;
        }
        public List<String> Name(List<String> names, string FoundName)
        {
            names = names.Where(x => x.Contains(FoundName)).ToList();
            return names;
        }
    }
}
