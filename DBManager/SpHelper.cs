using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealStateWebApi.DBManager
{
    public class SpHelper<T> where T : class
    {
        private static DBManager.RealStateEntities _context = new DBManager.RealStateEntities();
        public SpHelper()
        {
            _context = new DBManager.RealStateEntities();
        }
        public static IEnumerable<T> GetListWithRawSql(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(query, parameters).ToList();
        }
    }
}