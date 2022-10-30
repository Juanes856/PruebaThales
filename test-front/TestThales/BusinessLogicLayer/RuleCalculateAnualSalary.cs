using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public class RuleCalculateAnualSalary : IRule
    {
        public ParametersModel GetAnualSalary(ParametersModel parameters)
        {

            if (parameters.Employee_salary > 0)
            {
                parameters.Employee_salary = parameters.Employee_salary * 12;
            }

            return parameters;
        }
    }
}