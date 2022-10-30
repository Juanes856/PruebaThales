using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IRule
    {
        public ParametersModel GetAnualSalary(ParametersModel parameters);
    }
}
