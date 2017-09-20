using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DittoWS.Models;
using DittoWS.Helpers;

namespace DittoWS.Controllers
{
    [Produces("application/json")]
    public class CountryController : Controller
    {
        private readonly DittoContext _context;

        public CountryController(DittoContext context)
        {
            _context = context;
        }
        // Finish QuestionLogic Below by Friday or Saturday Confirm Meeting with Kevin Monday or Wednesday
        #region Attribute
        [Route("Attribute/All")]
        [HttpPost]
        public XAttribute Attribute_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.Attribute> lst = new List<Models.Attribute>(); // To look up the values
            XAttribute x = new XAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Input input = rq.ToObject<Attribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.Attribute.ToList();
                        x.Success = true;
                        x.Attributes = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Attributes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Attribute/Code")]
        [HttpPost]
        public XAttribute Attribute_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.Attribute a = new Models.Attribute();
            XAttribute x = new XAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Input input = rq.ToObject<Attribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code))
                        {
                            a = _context.Attribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code);
                            List<Models.Attribute> lst = new List<Models.Attribute>
                            {
                                a
                            };
                            x.Success = true;
                            x.Attributes = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Attributes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Attribute/Create")]
        [HttpPost]
        public XAttribute Attribute_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XAttribute x = new XAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Create input = rq.ToObject<Attribute_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code) && !string.IsNullOrEmpty(input.Attribute_Desc))
                        {
                            int exists = _context.Attribute.Where(y => y.Attribute_Code == input.Attribute_Code).Count();
                            if (exists == 0)
                            {
                                // Attribute does not exist, create it
                                Models.Attribute a = new Models.Attribute();
                                a.Attribute_Code = input.Attribute_Code;
                                a.Attribute_Desc = input.Attribute_Desc;

                                _context.Attribute.Add(a);
                                _context.SaveChanges();

                                List<Models.Attribute> lst = new List<Models.Attribute>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Attributes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code and Attribute_Desc must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating Attribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Attribute/Delete")]
        [HttpPost]
        public XAttribute Attribute_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XAttribute x = new XAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Input input = rq.ToObject<Attribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code))
                        {
                            Models.Attribute a = _context.Attribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code);
                            if (a != null)
                            {
                                // Attribute exists, delete it
                                _context.Attribute.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting Attribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Attribute/Modify")]
        [HttpPost]
        public XAttribute Attribute_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XAttribute x = new XAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Create input = rq.ToObject<Attribute_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code))
                        {
                            Models.Attribute a = _context.Attribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code);
                            if (a != null)
                            {
                                // Attribute exists, modfy it
                                if (!string.IsNullOrEmpty(input.Attribute_Desc))
                                {
                                    a.Attribute_Desc = input.Attribute_Desc;
                                }
                                else
                                {
                                    a.Attribute_Desc = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.Attribute> lst = new List<Models.Attribute>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Attributes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying Attribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region AuthToken
        [Route("AuthToken/Active")]
        [HttpPost]
        public XAuthToken AuthToken_GetActive([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<AuthToken> clst = new List<AuthToken>(); // To look up the values
            XAuthToken ret = new XAuthToken(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors
            Functions func = new Functions(_context); // Instantiate the functions with our context
            AuthToken_Input input = rq.ToObject<AuthToken_Input>(); // Parse the JSON into our class

            if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
            {

                Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                if (vat.Valid)
                {
                    try
                    {
                        clst = _context.AuthToken.Where(x => x.Expired == false).ToList();
                        List<AuthToken> lst = new List<AuthToken>();

                        foreach (AuthToken atoken in clst)
                        {
                            AuthToken token = new AuthToken();

                            token.AuthUser_ID = atoken.AuthUser_ID;
                            token.Token = atoken.Token;
                            token.Issued = atoken.Issued;
                            token.Last_Used = atoken.Last_Used;
                            token.Expired = atoken.Expired;

                            lst.Add(token);
                        }

                        ret.Success = true;
                        ret.AuthToken = lst;
                    }
                    catch (Exception ex)
                    {
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Unexpected error getting active AuthToken entries",
                            Exception = ex.Message.ToString()
                        };
                        if (ex.InnerException != null)
                        {
                            err.InnerException = ex.InnerException.Message.ToString();
                        }
                        err.Input = string.Empty;
                        error_list.Add(err);
                        ret.Success = false;
                    }
                }
                else
                {
                    // Invalid Token - Log in again
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "Invalid Token",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    ret.Success = false;
                }
            }
            else
            {
                // Missing token
                Error err = new Error()
                {
                    Code = "",
                    Text = "No Token provided",
                };
                if (rq != null)
                {
                    err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                }
                else
                {
                    err.Input = "Unable to get input";
                }
                error_list.Add(err);
                ret.Success = false;
            }
            ret.Errors = error_list;
            return ret;
        }
        #endregion
        #region MiscLocale
        [Route("MiscLocale/All")]
        [HttpPost]
        public XMiscLocale MiscLocale_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<MiscLocale> lst = new List<MiscLocale>(); // To look up the values
            XMiscLocale x = new XMiscLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Attribute_Input input = rq.ToObject<Attribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.MiscLocale.ToList();
                        x.Success = true;
                        x.MiscLocales = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Misc Locales",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("MiscLocale/Code")]
        [HttpPost]
        public XMiscLocale MiscLocale_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            MiscLocale a = new Models.MiscLocale();
            XMiscLocale x = new XMiscLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                MiscLocale_Input input = rq.ToObject<MiscLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Misc_Code) && !string.IsNullOrEmpty(input.Locale_Code))
                        {
                            a = _context.MiscLocale.FirstOrDefault(y => y.Misc_Code == input.Misc_Code && y.Locale_Code == input.Locale_Code);
                            List<MiscLocale> lst = new List<MiscLocale>
                            {
                                a
                            };
                            x.Success = true;
                            x.MiscLocales = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Misc_Code and Locale_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Misc Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("MiscLocale/Create")]
        [HttpPost]
        public XMiscLocale MiscLocale_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XMiscLocale x = new XMiscLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                MiscLocale_Create input = rq.ToObject<MiscLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Misc_Code) && !string.IsNullOrEmpty(input.Locale_Code) && !string.IsNullOrEmpty(input.Misc_Text))
                        {
                            int exists = _context.MiscLocale.Where(y => y.Misc_Code == input.Misc_Code && y.Locale_Code == input.Locale_Code).Count();
                            if (exists == 0)
                            {
                                // Misc Locale does not exist, create it
                                MiscLocale a = new MiscLocale();
                                a.Misc_Code = input.Misc_Code;
                                a.Locale_Code = input.Locale_Code;
                                a.Misc_Text = input.Misc_Text;

                                _context.MiscLocale.Add(a);
                                _context.SaveChanges();

                                List<MiscLocale> lst = new List<MiscLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.MiscLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Misc_Code and Locale_Code already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Misc_Code, Locale_Code and Misc_Text must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating Misc Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("MiscLocale/Delete")]
        [HttpPost]
        public XMiscLocale MiscLocale_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XMiscLocale x = new XMiscLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                MiscLocale_Input input = rq.ToObject<MiscLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Misc_Code) && !string.IsNullOrEmpty(input.Locale_Code))
                        {
                            MiscLocale a = _context.MiscLocale.FirstOrDefault(y => y.Misc_Code == input.Misc_Code && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc Locale exists, delete it
                                _context.MiscLocale.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Misc_Code and Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Misc_Code and Locale_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting Misc Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("MiscLocale/Modify")]
        [HttpPost]
        public XMiscLocale MiscLocale_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XMiscLocale x = new XMiscLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                MiscLocale_Create input = rq.ToObject<MiscLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Misc_Code) && !string.IsNullOrEmpty(input.Locale_Code))
                        {
                            MiscLocale a = _context.MiscLocale.FirstOrDefault(y => y.Misc_Code == input.Misc_Code && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc_Locale exists, modfy it
                                if (!string.IsNullOrEmpty(input.Misc_Text))
                                {
                                    a.Misc_Text = input.Misc_Text;
                                }
                                else
                                {
                                    a.Misc_Text = string.Empty;
                                }
                                _context.SaveChanges();

                                List<MiscLocale> lst = new List<MiscLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.MiscLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Misc_Code and Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Misc_Code and Locale_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying Misc Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region Question
        [Route("Question/All")]
        [HttpPost]
        public XQuestion Question_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Question> lst = new List<Question>(); // To look up the values
            XQuestion x = new XQuestion(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Question_Input input = rq.ToObject<Question_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.Question.ToList();
                        x.Success = true;
                        x.Questions = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Question",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Question/Code")]
        [HttpPost]
        public XQuestion Question_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Question a = new Models.Question();
            XQuestion x = new XQuestion(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                Question_Input input = rq.ToObject<Question_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0) //Change Made PRI KEY Check with Kevin
                        {
                            a = _context.Question.FirstOrDefault(y => y.Question_ID == input.Question_ID);//Check with Kevin 
                            List<Models.Question> lst = new List<Models.Question>
                            {
                                a
                            };
                            x.Success = true;
                            x.Questions = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID was not produced",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Misc Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Question/Create")]
        [HttpPost]
        public XQuestion Question_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestion x = new XQuestion(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Question_Create input = rq.ToObject<Question_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Question_Alias) && !string.IsNullOrEmpty(input.Default_Answer)  )//CHECk with Kevin
                        {
                            int exists = _context.Question.Where(y =>y.Question_ID == input.Question_ID).Count();
                            if (exists == 0)
                            {
                                // Question does not exist, create it
                                Question a = new Question(); // Column Values from DB set in Question.cs
                                a.Question_ID = input.Question_ID;
                                a.Survey_ID = input.Survey_ID;
                                a.Question_Alias = input.Question_Alias;
                                a.Question_Type_ID = input.Question_Type_ID;

                                a.Default_Answer = input.Default_Answer;
                                a.Is_Required = false;
                                a.Ord_By = input.Ord_By;
                                a.Is_Active = true;

                                _context.Question.Add(a);
                                _context.SaveChanges();

                                List<Question> lst = new List<Question>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Questions = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Alias, and Answer must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating Question",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Question/Delete")]
        [HttpPost]
        public XQuestion Question_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestion x = new XQuestion(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                Question_Input input = rq.ToObject<Question_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_ID > 0))
                        {
                            Question a = _context.Question.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // Misc Locale exists, delete it
                                _context.Question.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting Question",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Question/Modify")]
        [HttpPost]
        public XQuestion Question_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestion x = new XQuestion(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                Question_Create input = rq.ToObject<Question_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_ID > 0))
                        {
                            Question a = _context.Question.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // Misc_Locale exists, modfy it
                                if (!string.IsNullOrEmpty(input.Question_Alias) && !string.IsNullOrEmpty(input.Default_Answer)  && (input.Survey_ID > 0) && (input.Question_Type_ID > 0) && (input.Ord_By > 0) && (input.Is_Required == false) && input.Is_Active == true)
                                {
                                    a.Question_ID = input.Question_ID;
                                    a.Survey_ID = input.Survey_ID;
                                    a.Question_Alias = input.Question_Alias;
                                    a.Question_Type_ID = input.Question_Type_ID;

                                    a.Default_Answer = input.Default_Answer;
                                    a.Is_Required = input.Is_Required;
                                    a.Ord_By = input.Ord_By;
                                    a.Is_Active = input.Is_Active;
                                }
                                else
                                {
                                    a.Question_ID = 0;
                                    a.Survey_ID = 0;
                                    a.Question_Alias = string.Empty;
                                    a.Question_Type_ID = 0;

                                    a.Default_Answer = string.Empty;
                                    a.Is_Required = false;
                                    a.Ord_By = 0;
                                    a.Is_Active = true;
                                }
                                _context.SaveChanges();

                                List<Question> lst = new List<Question>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Questions = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying Question",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region QuestionAttribute
        [Route("QuestionAttribute/All")]
        [HttpPost]
        public XQuestionAttribute QuestionAttribute_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.QuestionAttribute> lst = new List<Models.QuestionAttribute>(); // To look up the values
            XQuestionAttribute x = new XQuestionAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionAttribute_Input input = rq.ToObject<QuestionAttribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionAttribute.ToList();
                        x.Success = true;
                        x.QuestionAttributes = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionAttributes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionAttribute/Code")]
        [HttpPost]
        public XQuestionAttribute QuestionAttribute_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionAttribute a = new Models.QuestionAttribute();
            XQuestionAttribute x = new XQuestionAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionAttribute_Input input = rq.ToObject<QuestionAttribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code) && input.Question_ID > 0)
                        {
                            a = _context.QuestionAttribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code && y.Question_ID == input.Question_ID);
                            List<Models.QuestionAttribute> lst = new List<Models.QuestionAttribute>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionAttributes = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code and Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionAttributes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionAttribute/Create")]
        [HttpPost]
        public XQuestionAttribute QuestionAttribute_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionAttribute x = new XQuestionAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionAttribute_Create input = rq.ToObject<QuestionAttribute_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code) && !string.IsNullOrEmpty(input.Attribute_Value))
                        {
                            int exists = _context.QuestionAttribute.Where(y => y.Attribute_Code == input.Attribute_Code && y.Question_ID == input.Question_ID).Count();
                            if (exists == 0)
                            {
                                // Attribute does not exist, create it
                                QuestionAttribute a = new QuestionAttribute();
                                a.Question_ID = input.Question_ID;
                                a.Attribute_Code = input.Attribute_Code;
                                a.Attribute_Value = input.Attribute_Value;
                                

                                _context.QuestionAttribute.Add(a);
                                _context.SaveChanges();

                                List<QuestionAttribute> lst = new List<QuestionAttribute>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionAttributes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code or Question_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code and Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionAttribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionAttribute/Delete")]
        [HttpPost]
        public XQuestionAttribute QuestionAttribute_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionAttribute x = new XQuestionAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionAttribute_Input input = rq.ToObject<QuestionAttribute_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code) && input.Question_ID > 0)
                        {
                            Models.QuestionAttribute a = _context.QuestionAttribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code && y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // Attribute exists, delete it
                                _context.QuestionAttribute.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code or Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code and Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionAttribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionAttribute/Modify")]
        [HttpPost]
        public XQuestionAttribute QuestionAttribute_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionAttribute x = new XQuestionAttribute(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionAttribute_Create input = rq.ToObject<QuestionAttribute_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Attribute_Code) && input.Question_ID > 0)
                        {
                            Models.QuestionAttribute a = _context.QuestionAttribute.FirstOrDefault(y => y.Attribute_Code == input.Attribute_Code && y.Question_ID == input.Question_ID );
                            if (a != null)
                            {
                                // Attribute exists, modfy it
                                if (!string.IsNullOrEmpty(input.Attribute_Value))
                                {
                                    a.Attribute_Value = input.Attribute_Value;
                                }
                                else
                                {
                                    a.Attribute_Value = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.QuestionAttribute> lst = new List<Models.QuestionAttribute>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionAttributes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Attribute_Code or Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Attribute_Code or Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionAttribute",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        #endregion
        #region QuestionLocale
        [Route("QuestionLocale/All")]
        [HttpPost]
        public XQuestionLocale QuestionLocale_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<QuestionLocale> lst = new List<QuestionLocale>(); // To look up the values
            XQuestionLocale x = new XQuestionLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLocale_Input input = rq.ToObject<QuestionLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionLocale.ToList();
                        x.Success = true;
                        x.QuestionLocales = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLocale/Code")]
        [HttpPost]
        public XQuestionLocale QuestionLocale_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            QuestionLocale a = new Models.QuestionLocale();
            XQuestionLocale x = new XQuestionLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionLocale_Input input = rq.ToObject<QuestionLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code)) //Change Made PRI KEY Check with Kevin
                        {
                            a = _context.QuestionLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID && y.Locale_Code == input.Locale_Code);//Check with Kevin 
                            List<Models.QuestionLocale> lst = new List<Models.QuestionLocale>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionLocales = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID  or Locale_Code was not produced",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Question Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLocale/Create")]
        [HttpPost]
        public XQuestionLocale QuestionLocale_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLocale x = new XQuestionLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLocale_Create input = rq.ToObject<QuestionLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (!string.IsNullOrEmpty(input.Question_Text) && !string.IsNullOrEmpty(input.Question_Subtext) && (input.Question_ID > 0) && !string.IsNullOrEmpty(input.Question_Short) && !string.IsNullOrEmpty(input.Locale_Code))//CHECk with Kevin
                        {
                            int exists = _context.QuestionLocale.Where(y => y.Question_ID == input.Question_ID && y.Locale_Code == input.Locale_Code).Count();
                            if (exists == 0)
                            {
                                // Question does not exist, create it
                                QuestionLocale a = new QuestionLocale(); // Column Values from DB set in Question.cs
                                a.Question_ID = input.Question_ID;
                                a.Locale_Code = input.Locale_Code;
                                a.Question_Text = input.Question_Text;
                                a.Question_Subtext = input.Question_Subtext;

                                a.Question_Short = input.Question_Short;
                               

                                _context.QuestionLocale.Add(a);
                                _context.SaveChanges();

                                List<QuestionLocale> lst = new List<QuestionLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "QuestionLocale already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Locale_Code, and Answer must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLocale/Delete")]
        [HttpPost]
        public XQuestionLocale QuestionLocale_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLocale x = new XQuestionLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionLocale_Input input = rq.ToObject<QuestionLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_ID > 0))
                        {
                            QuestionLocale a = _context.QuestionLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc Locale exists, delete it
                                _context.QuestionLocale.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID or Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID and Locale_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLocale/Modify")]
        [HttpPost]
        public XQuestionLocale QuestionLocale_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLocale x = new XQuestionLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionLocale_Create input = rq.ToObject<QuestionLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code))
                        {
                            QuestionLocale a = _context.QuestionLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc_Locale exists, modfy it
                                if (!string.IsNullOrEmpty(input.Question_Text) && !string.IsNullOrEmpty(input.Question_Subtext) && (input.Question_ID > 0) && !string.IsNullOrEmpty(input.Question_Short) && !string.IsNullOrEmpty(input.Locale_Code))
                                {
                                    a.Question_ID = input.Question_ID;
                                    a.Locale_Code = input.Locale_Code;
                                    a.Question_Text = input.Question_Text;
                                    a.Question_Subtext = input.Question_Subtext;

                                    a.Question_Short = input.Question_Short;

                                }
                                else
                                {
                                    a.Question_ID = 0;
                                    
                                    a.Question_Text = string.Empty;
                                    a.Question_Subtext = string.Empty;
                                    a.Question_Short = string.Empty;
                                    a.Locale_Code = string.Empty;
                                    
                                }
                                _context.SaveChanges();

                                List<QuestionLocale> lst = new List<QuestionLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID or Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID or Locale_Codemust be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        #endregion
        #region QuestionLogic
        [Route("QuestionLogic/All")]
        [HttpPost]
        public XQuestionLogic QuestionLogic_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<QuestionLogic> lst = new List<QuestionLogic>(); // To look up the values
            XQuestionLogic x = new XQuestionLogic(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLogic_Input input = rq.ToObject<QuestionLogic_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionLogic.ToList();
                        x.Success = true;
                        x.QuestionLogics = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLogic/Code")]
        [HttpPost]
        public XQuestionLogic QuestionLogic_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionLogic a = new Models.QuestionLogic();
            XQuestionLogic x = new XQuestionLogic(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLogic_Input input = rq.ToObject<QuestionLogic_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Logic_ID > 0)
                        {
                            a = _context.QuestionLogic.FirstOrDefault(y => y.Question_Logic_ID == input.Question_Logic_ID);
                            List<Models.QuestionLogic> lst = new List<Models.QuestionLogic>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionLogics = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionLogics",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLogic/Create")]
        [HttpPost]
        public XQuestionLogic QuestionLogic_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLogic x = new XQuestionLogic(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLogic_Create input = rq.ToObject<QuestionLogic_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Logic_ID > 0)
                        {
                            int exists = _context.QuestionLogic.Where(y => y.Question_Logic_ID == input.Question_Logic_ID).Count();
                            if (exists == 0)
                            {
                                // QuestionLogic does not exist, create it
                                Models.QuestionLogic a = new Models.QuestionLogic();
                                a.Question_ID = input.Question_ID;
                                a.Logic = input.Logic;
                                a.Target_Question_ID = input.Target_Question_ID;
                                a.Target_Visible = input.Target_Visible;
                                a.Target_Value = input.Target_Value;

        

        _context.QuestionLogic.Add(a);
                                _context.SaveChanges();

                                List<Models.QuestionLogic> lst = new List<Models.QuestionLogic>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionLogics = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Logic_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Logic_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionLogic",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLogic/Delete")]
        [HttpPost]
        public XQuestionLogic QuestionLogic_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLogic x = new XQuestionLogic(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLogic_Input input = rq.ToObject<QuestionLogic_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Logic_ID > 0)
                        {
                            Models.QuestionLogic a = _context.QuestionLogic.FirstOrDefault(y => y.Question_Logic_ID == input.Question_Logic_ID);
                            if (a != null)
                            {
                                // QuestionLogic exists, delete it
                                _context.QuestionLogic.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Logic_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Logic_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionLogic",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionLogic/Modify")]
        [HttpPost]
        public XQuestionLogic QuestionLogic_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionLogic x = new XQuestionLogic(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionLogic_Create input = rq.ToObject<QuestionLogic_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Logic_ID > 0)
                        {
                            Models.QuestionLogic a = _context.QuestionLogic.FirstOrDefault(y => y.Question_Logic_ID == input.Question_Logic_ID);
                            if (a != null)
                            {
                                // QuestionLogic exists, modfy it
                                if (input.Question_Logic_ID > 0)
                                {
                                    a.Question_Logic_ID = input.Question_Logic_ID;
                                }
                                else
                                {
                                    a.Question_ID = 1;
                                    a.Logic = string.Empty;
                                    a.Target_Question_ID = 1;
                                    a.Target_Visible = true;
                                    a.Target_Value = string.Empty;
       
    }
                                _context.SaveChanges();

                                List<Models.QuestionLogic> lst = new List<Models.QuestionLogic>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionLogics = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Logic_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Logic_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionLogic",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion 
        #region QuestionMulti
        [Route("QuestionMulti/All")]
        [HttpPost]
        public XQuestionMulti QuestionMulti_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<QuestionMulti> lst = new List<QuestionMulti>(); // To look up the values
            XQuestionMulti x = new XQuestionMulti(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMulti_Input input = rq.ToObject<QuestionMulti_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionMulti.ToList();
                        x.Success = true;
                        x.QuestionMultis = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionMultis",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMulti/Code")]
        [HttpPost]
        public XQuestionMulti QuestionMulti_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionMulti a = new Models.QuestionMulti();
            XQuestionMulti x = new XQuestionMulti(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMulti_Input input = rq.ToObject<QuestionMulti_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Multi_ID > 0)
                        {
                            a = _context.QuestionMulti.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID);
                            List<Models.QuestionMulti> lst = new List<Models.QuestionMulti>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionMultis = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Multi_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionMultis",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMulti/Create")]
        [HttpPost]
        public XQuestionMulti QuestionMulti_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMulti x = new XQuestionMulti(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMulti_Create input = rq.ToObject<QuestionMulti_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Multi_ID > 0)
                        {
                            int exists = _context.QuestionMulti.Where(y => y.Question_Multi_ID == input.Question_Multi_ID).Count();
                            if (exists == 0)
                            {
                                // QuestionMulti does not exist, create it
                                Models.QuestionMulti a = new Models.QuestionMulti();
                                a.Question_Multi_ID = input.Question_Multi_ID;
                                a.Question_ID = input.Question_ID;
                                a.Question_Multi_Alias = input.Question_Multi_Alias;
                                a.Has_Comment_Field = input.Has_Comment_Field;
                                a.Always_Show_Comment = input.Always_Show_Comment;
                                a.Ord_By = input.Ord_By;

        //                        public int Question_Multi_ID { get; set; } //pri
        //public int Question_ID { get; set; }
        //public string Question_Multi_Alias { get; set; }
        //public bool Has_Comment_Field { get; set; }
        //public bool Always_Show_Comment { get; set; }
        //public int Ord_By { get; set; }

        _context.QuestionMulti.Add(a);
                                _context.SaveChanges();

                                List<Models.QuestionMulti> lst = new List<Models.QuestionMulti>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionMultis = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Multi_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Multi_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionMulti",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMulti/Delete")]
        [HttpPost]
        public XQuestionMulti QuestionMulti_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMulti x = new XQuestionMulti(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMulti_Input input = rq.ToObject<QuestionMulti_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Multi_ID > 0)
                        {
                            Models.QuestionMulti a = _context.QuestionMulti.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID);
                            if (a != null)
                            {
                                // QuestionMulti exists, delete it
                                _context.QuestionMulti.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Multi_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Multi_Id must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionMulti",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMulti/Modify")]
        [HttpPost]
        public XQuestionMulti QuestionMulti_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMulti x = new XQuestionMulti(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMulti_Create input = rq.ToObject<QuestionMulti_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Multi_ID > 0)
                        {
                            Models.QuestionMulti a = _context.QuestionMulti.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID);
                            if (a != null)
                            {
                                // QuestionMulti exists, modfy it
                                if (input.Question_Multi_ID >0)
                                {
                                    a.Question_Multi_ID = input.Question_Multi_ID;
                                }
                                else
                                {
                                    a.Question_Multi_ID = 1;
                                    a.Question_ID = 1;
                                    a.Question_Multi_Alias = string.Empty;
                                    a.Has_Comment_Field = true;
                                    a.Always_Show_Comment = false;
                                    a.Ord_By = 1;
                                }
                                _context.SaveChanges();

                                List<Models.QuestionMulti> lst = new List<Models.QuestionMulti>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionMultis = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Multi_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Multi_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionMulti",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region QuestionMultiLocale
        [Route("QuestionMultiLocale/All")]
        [HttpPost]
        public XQuestionMultiLocale QuestionMultiLocale_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<QuestionMultiLocale> lst = new List<QuestionMultiLocale>(); // To look up the values
            XQuestionMultiLocale x = new XQuestionMultiLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMultiLocale_Input input = rq.ToObject<QuestionMultiLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionMultiLocale.ToList();
                        x.Success = true;
                        x.QuestionMultiLocales = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionMultiLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMultiLocale/Code")]
        [HttpPost]
        public XQuestionMultiLocale QuestionMultiLocale_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            QuestionMultiLocale a = new Models.QuestionMultiLocale();
            XQuestionMultiLocale x = new XQuestionMultiLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionMultiLocale_Input input = rq.ToObject<QuestionMultiLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_Multi_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code)) //Change Made PRI KEY Check with Kevin
                        {
                            a = _context.QuestionMultiLocale.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID && y.Locale_Code == input.Locale_Code);//Check with Kevin 
                            List<Models.QuestionMultiLocale> lst = new List<Models.QuestionMultiLocale>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionMultiLocales = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Multi_ID  or Locale_Code was not produced",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Question Locale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMultiLocale/Create")]
        [HttpPost]
        public XQuestionMultiLocale QuestionMultiLocale_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMultiLocale x = new XQuestionMultiLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionMultiLocale_Create input = rq.ToObject<QuestionMultiLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if  ((input.Question_Multi_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code))//CHECk with Kevin
                        {
                            int exists = _context.QuestionMultiLocale.Where(y => y.Question_Multi_ID == input.Question_Multi_ID && y.Locale_Code == input.Locale_Code).Count();
                            if (exists == 0)
                            {
                                // Question does not exist, create it
                                QuestionMultiLocale a = new QuestionMultiLocale(); // Column Values from DB set in Question.cs
                                a.Question_Multi_ID = input.Question_Multi_ID;
                                a.Locale_Code = input.Locale_Code;
                                


                                _context.QuestionMultiLocale.Add(a);
                                _context.SaveChanges();

                                List<QuestionMultiLocale> lst = new List<QuestionMultiLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionMultiLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "QuestionMultiLocale already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Locale_Code, and Answer must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionMultiLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMultiLocale/Delete")]
        [HttpPost]
        public XQuestionMultiLocale QuestionMultiLocale_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMultiLocale x = new XQuestionMultiLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionMultiLocale_Input input = rq.ToObject<QuestionMultiLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_Multi_ID > 0))
                        {
                            QuestionMultiLocale a = _context.QuestionMultiLocale.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc Locale exists, delete it
                                _context.QuestionMultiLocale.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID or Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID and Locale_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionMultiLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionMultiLocale/Modify")]
        [HttpPost]
        public XQuestionMultiLocale QuestionMultiLocale_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionMultiLocale x = new XQuestionMultiLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Functions func = new Functions(_context); // Instantiate the functions with our context
                QuestionMultiLocale_Create input = rq.ToObject<QuestionMultiLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Question_Multi_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code))
                        {
                            QuestionMultiLocale a = _context.QuestionMultiLocale.FirstOrDefault(y => y.Question_Multi_ID == input.Question_Multi_ID && y.Locale_Code == input.Locale_Code);
                            if (a != null)
                            {
                                // Misc_Locale exists, modfy it
                                if ( (input.Question_Multi_ID > 0) && !string.IsNullOrEmpty(input.Locale_Code))
                                {
                                    a.Question_Multi_ID = input.Question_Multi_ID;
                                    a.Locale_Code = input.Locale_Code;
                                    

                                }
                                else
                                {
                                    a.Question_Multi_ID = 1;
                                    a.Locale_Code = string.Empty;

                                }
                                _context.SaveChanges();

                                List<QuestionMultiLocale> lst = new List<QuestionMultiLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionMultiLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID or Locale_Code do not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID or Locale_Codemust be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionMultiLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region QuestionType
        [Route("QuestionType/All")]
        [HttpPost]
        public XQuestionType QuestionType_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.QuestionType> lst = new List<Models.QuestionType>(); // To look up the values
            XQuestionType x = new XQuestionType(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionType_Input input = rq.ToObject<QuestionType_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionType.ToList();
                        x.Success = true;
                        x.QuestionTypes = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionTypes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionType/Code")]
        [HttpPost]
        public XQuestionType QuestionType_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionType a = new Models.QuestionType();
            XQuestionType x = new XQuestionType(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionType_Input input = rq.ToObject<QuestionType_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Type_ID > 0)
                        {
                            a = _context.QuestionType.FirstOrDefault(y => y.Question_Type_ID == input.Question_Type_ID);
                            List<Models.QuestionType> lst = new List<Models.QuestionType>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionTypes = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "QuestionType_Code must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionTypes",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionType/Create")]
        [HttpPost]
        public XQuestionType QuestionType_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionType x = new XQuestionType(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionType_Create input = rq.ToObject<QuestionType_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Type_ID > 0)
                        {
                            int exists = _context.QuestionType.Where(y => y.Question_Type_ID == input.Question_Type_ID).Count();
                            if (exists == 0)
                            {
                                // QuestionType does not exist, create it
                                Models.QuestionType a = new Models.QuestionType();
                                a.Question_Type_ID = input.Question_Type_ID;
                                a.Question_Type_Name = input.Question_Type_Name;

                                _context.QuestionType.Add(a);
                                _context.SaveChanges();

                                List<Models.QuestionType> lst = new List<Models.QuestionType>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionTypes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "QUestion_Type_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Type_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionType",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionType/Delete")]
        [HttpPost]
        public XQuestionType QuestionType_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionType x = new XQuestionType(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionType_Input input = rq.ToObject<QuestionType_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Type_ID > 0)
                        {
                            Models.QuestionType a = _context.QuestionType.FirstOrDefault(y => y.Question_Type_ID == input.Question_Type_ID);
                            if (a != null)
                            {
                                // QuestionType exists, delete it
                                _context.QuestionType.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Type_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Type_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionType",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionType/Modify")]
        [HttpPost]
        public XQuestionType QuestionType_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionType x = new XQuestionType(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionType_Create input = rq.ToObject<QuestionType_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_Type_ID > 0)
                        {
                            Models.QuestionType a = _context.QuestionType.FirstOrDefault(y => y.Question_Type_ID == input.Question_Type_ID);
                            if (a != null)
                            {
                                // QuestionType exists, modfy it
                                if (input.Question_Type_ID > 0)
                                {
                                    a.Question_Type_ID = input.Question_Type_ID;
                                }
                                else
                                {
                                    a.Question_Type_ID = 1;
                                    a.Question_Type_Name = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.QuestionType> lst = new List<Models.QuestionType>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionTypes = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_Type_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_Type_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionType",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region QuestionValue
        [Route("QuestionValue/All")]
        [HttpPost]
        public XQuestionValue QuestionValue_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.QuestionValue> lst = new List<Models.QuestionValue>(); // To look up the values
            XQuestionValue x = new XQuestionValue(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValue_Input input = rq.ToObject<QuestionValue_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionValue.ToList();
                        x.Success = true;
                        x.QuestionValues = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionValues",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValue/Code")]
        [HttpPost]
        public XQuestionValue QuestionValue_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionValue a = new Models.QuestionValue();
            XQuestionValue x = new XQuestionValue(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValue_Input input = rq.ToObject<QuestionValue_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0 && input.Value_ID > 0)
                        {
                            a = _context.QuestionValue.FirstOrDefault(y => y.Question_ID == input.Question_ID && y.Value_ID == input.Value_ID);
                            List<Models.QuestionValue> lst = new List<Models.QuestionValue>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionValues = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionValues",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValue/Create")]
        [HttpPost]
        public XQuestionValue QuestionValue_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValue x = new XQuestionValue(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValue_Create input = rq.ToObject<QuestionValue_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0 && input.Value_ID > 0)
                        {
                            int exists = _context.QuestionValue.Where(y => y.Question_ID == input.Question_ID).Count();
                            if (exists == 0)
                            {
                                // QuestionValue does not exist, create it
                                Models.QuestionValue a = new Models.QuestionValue();
                                a.Question_ID = input.Question_ID;
                                a.Value_ID = input.Value_ID;
                                a.Value_Alias = input.Value_Alias;
                                a.Ord_By = input.Ord_By;
                                a.Is_Selected = input.Is_Selected;
                                 

                                _context.QuestionValue.Add(a);
                                _context.SaveChanges();

                                List<Models.QuestionValue> lst = new List<Models.QuestionValue>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionValues = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionValue",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValue/Delete")]
        [HttpPost]
        public XQuestionValue QuestionValue_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValue x = new XQuestionValue(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValue_Input input = rq.ToObject<QuestionValue_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            Models.QuestionValue a = _context.QuestionValue.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // QuestionValue exists, delete it
                                _context.QuestionValue.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionValue",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValue/Modify")]
        [HttpPost]
        public XQuestionValue QuestionValue_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValue x = new XQuestionValue(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValue_Create input = rq.ToObject<QuestionValue_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            Models.QuestionValue a = _context.QuestionValue.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // QuestionValue exists, modfy it
                                if ((input.Question_ID > 0))
                                {
                                    a.Question_ID = input.Question_ID;
                                }
                                else
                                {
                                    a.Question_ID = 1;
                                    a.Value_ID = 1;
                                    a.Value_Alias = string.Empty;
                                    a.Ord_By = 1;
                                    a.Is_Selected = true;
                                    
                                    
                                }
                                _context.SaveChanges();

                                List<Models.QuestionValue> lst = new List<Models.QuestionValue>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionValues = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionValue",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region QuestionValueLocale
        [Route("QuestionValueLocale/All")]
        [HttpPost]
        public XQuestionValueLocale QuestionValueLocale_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.QuestionValueLocale> lst = new List<Models.QuestionValueLocale>(); // To look up the values
            XQuestionValueLocale x = new XQuestionValueLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValueLocale_Input input = rq.ToObject<QuestionValueLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.QuestionValueLocale.ToList();
                        x.Success = true;
                        x.QuestionValueLocales = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionValueLocales",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValueLocale/Code")]
        [HttpPost]
        public XQuestionValueLocale QuestionValueLocale_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.QuestionValueLocale a = new Models.QuestionValueLocale();
            XQuestionValueLocale x = new XQuestionValueLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValueLocale_Input input = rq.ToObject<QuestionValueLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            a = _context.QuestionValueLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            List<Models.QuestionValueLocale> lst = new List<Models.QuestionValueLocale>
                            {
                                a
                            };
                            x.Success = true;
                            x.QuestionValueLocales = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting QuestionValueLocales",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValueLocale/Create")]
        [HttpPost]
        public XQuestionValueLocale QuestionValueLocale_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValueLocale x = new XQuestionValueLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValueLocale_Create input = rq.ToObject<QuestionValueLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            int exists = _context.QuestionValueLocale.Where(y => y.Question_ID == input.Question_ID).Count();
                            if (exists == 0)
                            {
                                // QuestionValueLocale does not exist, create it
                                Models.QuestionValueLocale a = new Models.QuestionValueLocale();
                                a.Question_ID = input.Question_ID;
                                a.Value_ID = input.Value_ID;
                                a.Locale_Code = input.Locale_Code;
                                a.Value_Text = input.Value_Text;

                                _context.QuestionValueLocale.Add(a);
                                _context.SaveChanges();

                                List<Models.QuestionValueLocale> lst = new List<Models.QuestionValueLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionValueLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating QuestionValueLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValueLocale/Delete")]
        [HttpPost]
        public XQuestionValueLocale QuestionValueLocale_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValueLocale x = new XQuestionValueLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValueLocale_Input input = rq.ToObject<QuestionValueLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            Models.QuestionValueLocale a = _context.QuestionValueLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // QuestionValueLocale exists, delete it
                                _context.QuestionValueLocale.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Question_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting QuestionValueLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("QuestionValueLocale/Modify")]
        [HttpPost]
        public XQuestionValueLocale QuestionValueLocale_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XQuestionValueLocale x = new XQuestionValueLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                QuestionValueLocale_Create input = rq.ToObject<QuestionValueLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Question_ID > 0)
                        {
                            Models.QuestionValueLocale a = _context.QuestionValueLocale.FirstOrDefault(y => y.Question_ID == input.Question_ID);
                            if (a != null)
                            {
                                // QuestionValueLocale exists, modfy it
                                if (input.Question_ID > 0)
                                {
                                    a.Question_ID = input.Question_ID;
                                }
                                else
                                {
                                    a.Question_ID = 1;
                                    a.Value_ID = 1;
                                    a.Locale_Code = string.Empty;
                                    a.Value_Text = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.QuestionValueLocale> lst = new List<Models.QuestionValueLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.QuestionValueLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Question_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "QUestion_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying QuestionValueLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region Response
        [Route("Response/All")]
        [HttpPost]
        public XResponse Response_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Response> lst = new List<Response>(); // To look up the values
            XResponse x = new XResponse(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Response_Input input = rq.ToObject<Response_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.Response.ToList();
                        x.Success = true;
                        x.Responses = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Response",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Response/Code")]
        [HttpPost]
        public XResponse Response_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.Response a = new Models.Response();
            XResponse x = new XResponse(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Response_Input input = rq.ToObject<Response_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            a = _context.Response.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            List<Models.Response> lst = new List<Models.Response>
                            {
                                a
                            };
                            x.Success = true;
                            x.Responses = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Responses",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Response/Create")]
        [HttpPost]
        public XResponse Response_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponse x = new XResponse(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Response_Create input = rq.ToObject<Response_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            int exists = _context.Response.Where(y => y.Response_ID == input.Response_ID).Count();
                            if (exists == 0)
                            {
                                // Response does not exist, create it
                                Models.Response a = new Models.Response();
                                a.Response_ID = input.Response_ID;
                                a.Subject_ID = input.Subject_ID;
                                a.Survey_ID = input.Survey_ID;
                                a.Start_DateTime = input.Start_DateTime;
                                a.End_DateTime = input.End_DateTime;
                                a.Locale_Code = input.Locale_Code;
                                a.Notes = input.Notes;
                                a.Is_Deleted = input.Is_Deleted;

                                _context.Response.Add(a);
                                _context.SaveChanges();

                                List<Models.Response> lst = new List<Models.Response>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Responses = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_Code already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating Response",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Response/Delete")]
        [HttpPost]
        public XResponse Response_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponse x = new XResponse(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Response_Input input = rq.ToObject<Response_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.Response a = _context.Response.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // Response exists, delete it
                                _context.Response.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting Response",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Response/Modify")]
        [HttpPost]
        public XResponse Response_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponse x = new XResponse(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Response_Create input = rq.ToObject<Response_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.Response a = _context.Response.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // Response exists, modfy it
                                if (input.Response_ID != Guid.Empty)
                                {
                                    a.Response_ID = input.Response_ID;
                                    a.Subject_ID = input.Subject_ID;
                                    a.Survey_ID = input.Survey_ID;
                                    a.Start_DateTime = input.Start_DateTime;
                                    a.End_DateTime = input.End_DateTime;
                                    a.Locale_Code = input.Locale_Code;
                                    a.Notes = input.Notes;
                                    a.Is_Deleted = input.Is_Deleted;
                                }
                                else
                                {
                                    //a.Response_ID = 1;
                                    a.Subject_ID = string.Empty;
                                    a.Survey_ID = 1;
                                    a.Start_DateTime = DateTime.Now;
                                    a.End_DateTime = DateTime.Now;
                                    a.Locale_Code = string.Empty;
                                    a.Notes = string.Empty;
                                    a.Is_Deleted = false;
                                }
                                _context.SaveChanges();

                                List<Models.Response> lst = new List<Models.Response>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Responses = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_Code does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Reponse_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying Response",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region ResponseAlert
        [Route("ResponseAlert/All")]
        [HttpPost]
        public XResponseAlert ResponseAlert_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.ResponseAlert> lst = new List<Models.ResponseAlert>(); // To look up the values
            XResponseAlert x = new XResponseAlert(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseAlert_Input input = rq.ToObject<ResponseAlert_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.ResponseAlert.ToList();
                        x.Success = true;
                        x.ResponseAlerts = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseAlerts",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseAlert/Code")]
        [HttpPost]
        public XResponseAlert ResponseAlert_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.ResponseAlert a = new Models.ResponseAlert();
            XResponseAlert x = new XResponseAlert(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseAlert_Input input = rq.ToObject<ResponseAlert_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            a = _context.ResponseAlert.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            List<Models.ResponseAlert> lst = new List<Models.ResponseAlert>
                            {
                                a
                            };
                            x.Success = true;
                            x.ResponseAlerts = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseAlerts",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseAlert/Create")]
        [HttpPost]
        public XResponseAlert ResponseAlert_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseAlert x = new XResponseAlert(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseAlert_Create input = rq.ToObject<ResponseAlert_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            int exists = _context.ResponseAlert.Where(y => y.Response_ID == input.Response_ID).Count();
                            if (exists == 0)
                            {
                                // ResponseAlert does not exist, create it
                                Models.ResponseAlert a = new Models.ResponseAlert();
                                a.Response_ID = input.Response_ID;
                                a.EmailQueue_ID = input.EmailQueue_ID;
                                a.AuthUser_ID = input.AuthUser_ID;
                                a.Acknowledged = input.Acknowledged;

                                _context.ResponseAlert.Add(a);
                                _context.SaveChanges();

                                List<Models.ResponseAlert> lst = new List<Models.ResponseAlert>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseAlerts = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating ResponseAlert",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseAlert/Delete")]
        [HttpPost]
        public XResponseAlert ResponseAlert_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseAlert x = new XResponseAlert(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseAlert_Input input = rq.ToObject<ResponseAlert_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.ResponseAlert a = _context.ResponseAlert.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // ResponseAlert exists, delete it
                                _context.ResponseAlert.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting ResponseAlert",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseAlert/Modify")]
        [HttpPost]
        public XResponseAlert ResponseAlert_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseAlert x = new XResponseAlert(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseAlert_Create input = rq.ToObject<ResponseAlert_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.ResponseAlert a = _context.ResponseAlert.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // Attribute exists, modfy it
                                if (input.Response_ID != Guid.Empty)
                                {
                                    a.Response_ID = input.Response_ID;
                                    a.EmailQueue_ID = input.EmailQueue_ID;
                                    a.AuthUser_ID = input.AuthUser_ID;
                                    a.Acknowledged = input.Acknowledged;
                                }
                                else
                                {
                                    a.EmailQueue_ID = 1;
                                    a.AuthUser_ID = 1;
                                    a.Acknowledged = DateTime.Now;
                                }
                                _context.SaveChanges();

                                List<Models.ResponseAlert> lst = new List<Models.ResponseAlert>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseAlerts = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying ResponseAlert",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region ResponseHistory
        [Route("ResponseHistory/All")]
        [HttpPost]
        public XResponseHistory ResponseHistory_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.ResponseHistory> lst = new List<Models.ResponseHistory>(); // To look up the values
            XResponseHistory x = new XResponseHistory(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseHistory_Input input = rq.ToObject<ResponseHistory_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.ResponseHistory.ToList();
                        x.Success = true;
                        x.ResponseHistorys = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseHistorys",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseHistory/Code")]
        [HttpPost]
        public XResponseHistory ResponseHistory_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.ResponseHistory a = new Models.ResponseHistory();
            XResponseHistory x = new XResponseHistory(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseHistory_Input input = rq.ToObject<ResponseHistory_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_History_ID != Guid.Empty)
                        {
                            a = _context.ResponseHistory.FirstOrDefault(y => y.Response_History_ID == input.Response_History_ID);
                            List<Models.ResponseHistory> lst = new List<Models.ResponseHistory>
                            {
                                a
                            };
                            x.Success = true;
                            x.ResponseHistorys = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_History_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseHistorys",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseHistory/Create")]
        [HttpPost]
        public XResponseHistory ResponseHistory_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseHistory x = new XResponseHistory(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseHistory_Create input = rq.ToObject<ResponseHistory_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_History_ID != Guid.Empty)
                        {
                            int exists = _context.ResponseHistory.Where(y => y.Response_History_ID == input.Response_History_ID).Count();
                            if (exists == 0)
                            {
                                // ResponseHistory does not exist, create it
                                Models.ResponseHistory a = new Models.ResponseHistory();
                                a.Response_History_ID = input.Response_History_ID;
                                a.Response_Item_ID = input.Response_Item_ID;
                                a.Old_Answer = input.Old_Answer;
                                a.Edit_AuthUser_ID = input.Edit_AuthUser_ID;
                                a.Edited = input.Edited;

                                _context.ResponseHistory.Add(a);
                                _context.SaveChanges();

                                List<Models.ResponseHistory> lst = new List<Models.ResponseHistory>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseHistorys = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_History_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_History_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating ResponseHistory",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseHistory/Delete")]
        [HttpPost]
        public XResponseHistory ResponseHistory_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseHistory x = new XResponseHistory(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseHistory_Input input = rq.ToObject<ResponseHistory_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_History_ID != Guid.Empty)
                        {
                            Models.ResponseHistory a = _context.ResponseHistory.FirstOrDefault(y => y.Response_History_ID == input.Response_History_ID);
                            if (a != null)
                            {
                                // ResponseHistory exists, delete it
                                _context.ResponseHistory.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_History_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_History_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting ResponseHistory",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseHistory/Modify")]
        [HttpPost]
        public XResponseHistory ResponseHistory_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseHistory x = new XResponseHistory(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseHistory_Create input = rq.ToObject<ResponseHistory_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_History_ID != Guid.Empty)
                        {
                            Models.ResponseHistory a = _context.ResponseHistory.FirstOrDefault(y => y.Response_History_ID == input.Response_History_ID);
                            if (a != null)
                            {
                                // ResponseHistory exists, modfy it
                                if (input.Response_History_ID != Guid.Empty)
                                {
                                    a.Response_History_ID = input.Response_History_ID;
                                    a.Response_Item_ID = input.Response_Item_ID;
                                    a.Old_Answer = input.Old_Answer;
                                    a.Edit_AuthUser_ID= input.Edit_AuthUser_ID;
                                    a.Edited = input.Edited;
                                }
                                else
                                {
                                    
                                    a.Response_Item_ID = Guid.Empty;
                                    a.Old_Answer = string.Empty;
                                    a.Edit_AuthUser_ID = 1;
                                    a.Edited = DateTime.Now;
                                }
                                _context.SaveChanges();

                                List<Models.ResponseHistory> lst = new List<Models.ResponseHistory>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseHistorys = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_History_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_History_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying ResponseHistory",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region ResponseInvalidSubject
        [Route("ResponseInvalidSubject/All")]
        [HttpPost]
        public XResponseInvalidSubject ResponseInvalidSubject_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.ResponseInvalidSubject> lst = new List<Models.ResponseInvalidSubject>(); // To look up the values
            XResponseInvalidSubject x = new XResponseInvalidSubject(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseInvalidSubject_Input input = rq.ToObject<ResponseInvalidSubject_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.ResponseInvalidSubject.ToList();
                        x.Success = true;
                        x.ResponseInvalidSubjects = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseInvalidSubjects",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseInvalidSubject/Code")]
        [HttpPost]
        public XResponseInvalidSubject ResponseInvalidSubject_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.ResponseInvalidSubject a = new Models.ResponseInvalidSubject();
            XResponseInvalidSubject x = new XResponseInvalidSubject(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseInvalidSubject_Input input = rq.ToObject<ResponseInvalidSubject_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            a = _context.ResponseInvalidSubject.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            List<Models.ResponseInvalidSubject> lst = new List<Models.ResponseInvalidSubject>
                            {
                                a
                            };
                            x.Success = true;
                            x.ResponseInvalidSubjects = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseInvalidSubjects",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseInvalidSubject/Create")]
        [HttpPost]
        public XResponseInvalidSubject ResponseInvalidSubject_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseInvalidSubject x = new XResponseInvalidSubject(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseInvalidSubject_Create input = rq.ToObject<ResponseInvalidSubject_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            int exists = _context.ResponseInvalidSubject.Where(y => y.Response_ID == input.Response_ID).Count();
                            if (exists == 0)
                            {
                                // ResponseInvalidSubject does not exist, create it
                                Models.ResponseInvalidSubject a = new Models.ResponseInvalidSubject();
                                a.Response_ID = input.Response_ID;
                                a.Subject_ID = input.Subject_ID;
                                a.EmailQueue_ID = input.EmailQueue_ID;
                                a.AuthUser_ID = input.AuthUser_ID;
                                a.Acknowledged = input.Acknowledged;

                                _context.ResponseInvalidSubject.Add(a);
                                _context.SaveChanges();

                                List<Models.ResponseInvalidSubject> lst = new List<Models.ResponseInvalidSubject>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseInvalidSubjects = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating ResponseInvalidSubject",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseInvalidSubject/Delete")]
        [HttpPost]
        public XResponseInvalidSubject ResponseInvalidSubject_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseInvalidSubject x = new XResponseInvalidSubject(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseInvalidSubject_Input input = rq.ToObject<ResponseInvalidSubject_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.ResponseInvalidSubject a = _context.ResponseInvalidSubject.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // ResponseInvalidSubject exists, delete it
                                _context.ResponseInvalidSubject.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting ResponseInvalidSubject",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseInvalidSubject/Modify")]
        [HttpPost]
        public XResponseInvalidSubject ResponseInvalidSubject_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseInvalidSubject x = new XResponseInvalidSubject(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseInvalidSubject_Create input = rq.ToObject<ResponseInvalidSubject_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_ID != Guid.Empty)
                        {
                            Models.ResponseInvalidSubject a = _context.ResponseInvalidSubject.FirstOrDefault(y => y.Response_ID == input.Response_ID);
                            if (a != null)
                            {
                                // ResponseInvalidSubject exists, modfy it
                                if (input.Response_ID !=  Guid.Empty)
                                {
                                    a.Response_ID = input.Response_ID;
                                    a.Subject_ID = input.Subject_ID;
                                    a.EmailQueue_ID = input.EmailQueue_ID;
                                    a.AuthUser_ID = input.AuthUser_ID;
                                    a.Acknowledged = input.Acknowledged;
                                }
                                else
                                {
                                    a.Subject_ID = string.Empty;
                                    a.EmailQueue_ID = 1;
                                    a.AuthUser_ID = 1;
                                    a.Acknowledged = DateTime.Now;
                                }
                                _context.SaveChanges();

                                List<Models.ResponseInvalidSubject> lst = new List<Models.ResponseInvalidSubject>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseInvalidSubjects = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying ResponseInvalidSubject",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion

        #region ResponseItem
        [Route("ResponseItem/All")]
        [HttpPost]
        public XResponseItem ResponseItem_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.ResponseItem> lst = new List<Models.ResponseItem>(); // To look up the values
            XResponseItem x = new XResponseItem(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseItem_Input input = rq.ToObject<ResponseItem_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.ResponseItem.ToList();
                        x.Success = true;
                        x.ResponseItems = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseItems",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseItem/Code")]
        [HttpPost]
        public XResponseItem ResponseItem_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.ResponseItem a = new Models.ResponseItem();
            XResponseItem x = new XResponseItem(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseItem_Input input = rq.ToObject<ResponseItem_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Response_Item_ID  > 0)
                        {
                            a = _context.ResponseItem.FirstOrDefault(y => y.Response_Item_ID == input.Response_Item_ID);
                            List<Models.ResponseItem> lst = new List<Models.ResponseItem>
                            {
                                a
                            };
                            x.Success = true;
                            x.ResponseItems = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_Item_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting ResponseItems",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseItem/Create")]
        [HttpPost]
        public XResponseItem ResponseItem_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseItem x = new XResponseItem(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseItem_Create input = rq.ToObject<ResponseItem_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Response_Item_ID > 0))
                        {
                            int exists = _context.ResponseItem.Where(y => y.Response_Item_ID == input.Response_Item_ID).Count();
                            if (exists == 0)
                            {
                                // ResponseItem does not exist, create it
                                Models.ResponseItem a = new Models.ResponseItem();
                                a.Response_Item_ID = input.Response_Item_ID;
                                a.Response_ID = input.Response_ID;
                                a.Question_ID = input.Question_ID;
                                a.Question_Multi_ID = input.Question_Multi_ID;
                                a.Additional_ID = input.Additional_ID;
                                a.Question_Alias = input.Question_Alias;
                                a.Answer = input.Answer;
                       

                                _context.ResponseItem.Add(a);
                                _context.SaveChanges();

                                List<Models.ResponseItem> lst = new List<Models.ResponseItem>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseItems = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_Item_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating ResponseItem",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseItem/Delete")]
        [HttpPost]
        public XResponseItem ResponseItem_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseItem x = new XResponseItem(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseItem_Input input = rq.ToObject<ResponseItem_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Response_Item_ID > 0))
                        {
                            Models.ResponseItem a = _context.ResponseItem.FirstOrDefault(y => y.Response_Item_ID == input.Response_Item_ID);
                            if (a != null)
                            {
                                // ResponseItem exists, delete it
                                _context.ResponseItem.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_Item_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_Item_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting ResponseItem",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("ResponseItem/Modify")]
        [HttpPost]
        public XResponseItem ResponseItem_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XResponseItem x = new XResponseItem(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                ResponseItem_Create input = rq.ToObject<ResponseItem_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if ((input.Response_Item_ID > 0))
                        {
                            Models.ResponseItem a = _context.ResponseItem.FirstOrDefault(y => y.Response_Item_ID == input.Response_Item_ID);
                            if (a != null)
                            {
                                // ResponseItem exists, modfy it
                                if ((input.Response_Item_ID > 0))
                                {
                                    a.Response_Item_ID = input.Response_Item_ID;
                                    a.Response_ID = input.Response_ID;
                                    a.Question_ID = input.Question_ID;
                                    a.Question_Multi_ID = input.Question_Multi_ID;
                                    a.Additional_ID = input.Additional_ID;
                                    a.Question_Alias = input.Question_Alias;
                                    a.Answer = input.Answer;
                                }
                                else
                                {
                                    a.Response_ID = Guid.Empty;
                                    a.Question_ID = 1;
                                    a.Question_Multi_ID = 1;
                                    a.Additional_ID = 1;
                                    a.Question_Alias = string.Empty;
                                    a.Answer = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.ResponseItem> lst = new List<Models.ResponseItem>
                                {
                                    a
                                };
                                x.Success = true;
                                x.ResponseItems = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Response_Item_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Response_Item_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying ResponseItem",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region Survey
        [Route("Survey/All")]
        [HttpPost]
        public XSurvey Survey_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.Survey> lst = new List<Models.Survey>(); // To look up the values
            XSurvey x = new XSurvey(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Survey_Input input = rq.ToObject<Survey_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.Survey.ToList();
                        x.Success = true;
                        x.Surveys = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Surveys",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Survey/Code")]
        [HttpPost]
        public XSurvey Survey_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.Survey a = new Models.Survey();
            XSurvey x = new XSurvey(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Survey_Input input = rq.ToObject<Survey_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            a = _context.Survey.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            List<Models.Survey> lst = new List<Models.Survey>
                            {
                                a
                            };
                            x.Success = true;
                            x.Surveys = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting Surveys",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Survey/Create")]
        [HttpPost]
        public XSurvey Survey_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurvey x = new XSurvey(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Survey_Create input = rq.ToObject<Survey_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            int exists = _context.Survey.Where(y => y.Survey_ID == input.Survey_ID).Count();
                            if (exists == 0)
                            {
                                // Survey does not exist, create it
                                Models.Survey a = new Models.Survey();
                                a.Survey_ID = input.Survey_ID;
                                a.Survey_Alias = input.Survey_Alias;
                                a.Is_Active = input.Is_Active;
                                a.Addtl_JQuery = input.Addtl_JQuery;
                                a.Task_ID = input.Task_ID;
                                a.Score_Limit = input.Score_Limit;

                                _context.Survey.Add(a);
                                _context.SaveChanges();

                                List<Models.Survey> lst = new List<Models.Survey>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Surveys = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating Survey",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Survey/Delete")]
        [HttpPost]
        public XSurvey Survey_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurvey x = new XSurvey(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Survey_Input input = rq.ToObject<Survey_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            Models.Survey a = _context.Survey.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            if (a != null)
                            {
                                // Survey exists, delete it
                                _context.Survey.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting Survey",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("Survey/Modify")]
        [HttpPost]
        public XSurvey Survey_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurvey x = new XSurvey(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                Survey_Create input = rq.ToObject<Survey_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            Models.Survey a = _context.Survey.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            if (a != null)
                            {
                                // Survey exists, modfy it
                                if (input.Survey_ID > 0)
                                {
                                    a.Survey_ID = input.Survey_ID;
                                    a.Survey_Alias = input.Survey_Alias;
                                    a.Is_Active = input.Is_Active;
                                    a.Addtl_JQuery = input.Addtl_JQuery;
                                    a.Task_ID = input.Task_ID;
                                    a.Score_Limit = input.Score_Limit;
                                }
                                else
                                {
                                    a.Survey_Alias = string.Empty;
                                    a.Is_Active = true;
                                    a.Addtl_JQuery = string.Empty;
                                    a.Task_ID = 1;
                                    a.Score_Limit = 01;
                                }
                                _context.SaveChanges();

                                List<Models.Survey> lst = new List<Models.Survey>
                                {
                                    a
                                };
                                x.Success = true;
                                x.Surveys = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying Survey",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
        #endregion
        #region SurveyLocale
        [Route("SurveyLocale/All")]
        [HttpPost]
        public XSurveyLocale SurveyLocale_GetAll([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            List<Models.SurveyLocale> lst = new List<Models.SurveyLocale>(); // To look up the values
            XSurveyLocale x = new XSurveyLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                SurveyLocale_Input input = rq.ToObject<SurveyLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        lst = _context.SurveyLocale.ToList();
                        x.Success = true;
                        x.SurveyLocales = lst;
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting SurveyLocales",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("SurveyLocale/Code")]
        [HttpPost]
        public XSurveyLocale SurveyLocale_GetByCode([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            Models.SurveyLocale a = new Models.SurveyLocale();
            XSurveyLocale x = new XSurveyLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                SurveyLocale_Input input = rq.ToObject<SurveyLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            a = _context.SurveyLocale.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            List<Models.SurveyLocale> lst = new List<Models.SurveyLocale>
                            {
                                a
                            };
                            x.Success = true;
                            x.SurveyLocales = lst;
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error getting SurveyLocales",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("SurveyLocale/Create")]
        [HttpPost]
        public XSurveyLocale SurveyLocale_Create([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurveyLocale x = new XSurveyLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                SurveyLocale_Create input = rq.ToObject<SurveyLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            int exists = _context.SurveyLocale.Where(y => y.Survey_ID == input.Survey_ID).Count();
                            if (exists == 0)
                            {
                                // SurveyLocale does not exist, create it
                                Models.SurveyLocale a = new Models.SurveyLocale();
                                a.Survey_ID = input.Survey_ID;
                                a.Locale_Code = input.Locale_Code;
                                a.Survey_Name = input.Survey_Name;
                                a.Survey_ShortName = input.Survey_ShortName;

                                _context.SurveyLocale.Add(a);
                                _context.SaveChanges();

                                List<Models.SurveyLocale> lst = new List<Models.SurveyLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.SurveyLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID already exists",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error creating SurveyLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("SurveyLocale/Delete")]
        [HttpPost]
        public XSurveyLocale SurveyLocale_Delete([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurveyLocale x = new XSurveyLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                SurveyLocale_Input input = rq.ToObject<SurveyLocale_Input>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            Models.SurveyLocale a = _context.SurveyLocale.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            if (a != null)
                            {
                                // SurveyLocale exists, delete it
                                _context.SurveyLocale.Remove(a);
                                _context.SaveChanges();

                                x.Success = true;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error deleting SurveyLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }

        [Route("SurveyLocale/Modify")]
        [HttpPost]
        public XSurveyLocale SurveyLocale_Modify([FromBody] Newtonsoft.Json.Linq.JToken rq)
        {
            XSurveyLocale x = new XSurveyLocale(); // Return value
            List<Error> error_list = new List<Error>(); // Store errors

            try
            {
                Helpers.Functions func = new Helpers.Functions(_context); // Instantiate the functions with our context
                SurveyLocale_Create input = rq.ToObject<SurveyLocale_Create>(); // Parse the JSON into our class
                if (!string.IsNullOrEmpty(input.Token)) // Check if Token exists
                {
                    Models.FunctionClasses.ValidAuthToken vat = func.ValidToken(input.Token); // Populate a valid token check
                    if (vat.Valid)
                    {
                        if (input.Survey_ID > 0)
                        {
                            Models.SurveyLocale a = _context.SurveyLocale.FirstOrDefault(y => y.Survey_ID == input.Survey_ID);
                            if (a != null)
                            {
                                // SurveyLocale exists, modfy it
                                if (input.Survey_ID > 0)
                                {
                                    a.Survey_ID = input.Survey_ID;
                                    a.Locale_Code = input.Locale_Code;
                                    a.Survey_Name = input.Survey_Name;
                                    a.Survey_ShortName = input.Survey_ShortName;
                                }
                                else
                                {
                                    a.Locale_Code = string.Empty;
                                    a.Survey_Name = string.Empty;
                                    a.Survey_ShortName = string.Empty;
                                }
                                _context.SaveChanges();

                                List<Models.SurveyLocale> lst = new List<Models.SurveyLocale>
                                {
                                    a
                                };
                                x.Success = true;
                                x.SurveyLocales = lst;
                            }
                            else
                            {
                                Error err = new Error()
                                {
                                    Code = "",
                                    Text = "Survey_ID does not exist",
                                    Exception = string.Empty,
                                    InnerException = string.Empty,
                                    Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                                };
                                error_list.Add(err);
                                x.Success = false;
                            }
                        }
                        else
                        {
                            Error err = new Error()
                            {
                                Code = "",
                                Text = "Survey_ID must be provided",
                                Exception = string.Empty,
                                InnerException = string.Empty,
                                Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq)
                            };
                            error_list.Add(err);
                            x.Success = false;
                        }
                    }
                    else
                    {
                        // Invalid Token - Log in again
                        Error err = new Error()
                        {
                            Code = "",
                            Text = "Invalid Token",
                        };
                        if (rq != null)
                        {
                            err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                        }
                        else
                        {
                            err.Input = "Unable to get input";
                        }
                        error_list.Add(err);
                        x.Success = false;
                    }
                }
                else
                {
                    // Missing token
                    Error err = new Error()
                    {
                        Code = "",
                        Text = "No Token provided",
                    };
                    if (rq != null)
                    {
                        err.Input = Newtonsoft.Json.JsonConvert.SerializeObject(rq);
                    }
                    else
                    {
                        err.Input = "Unable to get input";
                    }
                    error_list.Add(err);
                    x.Success = false;
                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected error modifying SurveyLocale",
                    Exception = ex.Message.ToString()
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = ex.InnerException.Message.ToString();
                }
                err.Input = string.Empty;
                error_list.Add(err);
                x.Success = false;
            }
            x.Errors = error_list;
            return x;
        }
# endregion



    }
}