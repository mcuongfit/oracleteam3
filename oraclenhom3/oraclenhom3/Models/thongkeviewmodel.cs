using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oraclenhom3.Models
{
    public class thongkeviewmodel
    {
        public int Nam { get; set; }
        public string tennuoc { get; set; }
        public List<double> nhietdo { get; set; }
        public List<double> luongmua { get; set; }
        public List<double> apsuat { get; set; }
        public List<double> tmax { get; set; }
        public List<double> tmin { get; set; }

    }
}