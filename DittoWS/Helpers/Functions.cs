using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DittoWS.Models;

namespace DittoWS.Helpers
{
    public class Functions
    {
        private readonly DittoContext _context;

        public Functions(DittoContext context)
        {
            _context = context;
        }
        public FunctionClasses.ValidAuthToken ValidToken(string token)
        {
            FunctionClasses.ValidAuthToken vat = new FunctionClasses.ValidAuthToken()
            {
                Valid = false
            };
            List<Error> error_list = new List<Error>();

            try
            {
                AuthToken atoken = _context.AuthToken.FirstOrDefault(x => x.Token == token && x.Expired == false);

                if (atoken != null)
                {
                    atoken.Last_Used = DateTime.UtcNow; // Update the Last_Used field so we can keep the token from being expired
                    _context.SaveChanges(); // Save the changes

                    // Fill the output
                    vat.Valid = true;
                    vat.AuthUser_ID = atoken.AuthUser_ID;
                    vat.Token = atoken.Token;
                    vat.Issued = atoken.Issued;
                    vat.Last_Used = atoken.Last_Used;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Error err = new Error()
                {
                    Code = "",
                    Text = "Unexpected Error while checking if token is valid",
                    Exception = ex.Message,
                    Input = token
                };
                if (ex.InnerException != null)
                {
                    err.InnerException = string.Empty;
                }
                error_list.Add(err);
            }

            vat.Errors = error_list;
            return vat;
        }
    }
}
