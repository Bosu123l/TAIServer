using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TAIServer.Entities.DataAccess;

namespace TAI.Services
{
    public class BaseService
    {
        public readonly DataContext DataContext;
        public BaseService(DataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("DataContext cannot be null!");
            }
            DataContext = dataContext;
        }
    }
}
