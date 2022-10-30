using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestThales
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IRule _Rule;

        
        [HttpGet]
        public async Task<BaseModel> GetEmployees()
        {
            BaseModel baseModel = new BaseModel();
            ResponseModel responseModel = new ResponseModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"http://dummy.restapiexample.com/api/v1/employees");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        baseModel = JsonConvert.DeserializeObject<BaseModel>(result);

                        foreach (var item in baseModel.data)
                        {
                            ParametersModel parameter = new ParametersModel()
                            {
                                Employee_salary = item.employee_salary
                            };

                            parameter = (ParametersModel)GetAnualSalry(parameter);

                            responseModel.anualSalary = parameter.Employee_salary;

                            
                        }


                    }
                    else
                    {
                        baseModel = new BaseModel
                        {
                            status = "failed",
                            data = { },
                            message = ""

                        };
                    }

                }
            }
            catch (Exception ex)
            {
                baseModel = new BaseModel
                {
                    status = "failed",
                    data = { },
                    message = ex.Message

                };
            }
            return baseModel;

        }

        [Route("id/{id:int}")]
        public async Task<SingleBaseModel> GetEmployee(int id)
        {
            SingleBaseModel singleModel = new SingleBaseModel();
            ResponseModel responseModel = new ResponseModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync($"http://dummy.restapiexample.com/api/v1/employee/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        singleModel = JsonConvert.DeserializeObject<SingleBaseModel>(result);

                    }
                    else
                    {
                        singleModel = new SingleBaseModel
                        {
                            status = "failed",
                            data = { },
                            message = ""

                        };
                    }

                }
            }
            catch (Exception ex)
            {
                singleModel = new SingleBaseModel
                {
                    status = "failed",
                    data = { },
                    message = ex.Message

                };
            }
            return singleModel;

        }

        public IActionResult GetAnualSalry(ParametersModel employee_salary)
        {
            try
            {
                return Ok(_Rule.GetAnualSalary(employee_salary));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        

    }
}
