using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Equation
    {
        public int Id { get; set; }
        public string EquationId { get; set; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }
        public string? Operation { get; set; }
        public double Result { get; set; }
    }
}
