using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class PagingHelper
    {
        private const int MaxPageSize = 50;

        private static int Quantity(int debut, int fin) => fin - debut + 1;

        public static IEnumerable<T> ProcessGet<T>(IQueryCollection queryParams, IEnumerable<T> entities, int entityCOunt, IHeaderDictionary responseHeader)
        {
            var premier = 0;
            var dernier = MaxPageSize;
            
            if (queryParams.ContainsKey("range"))
            {
                var range = queryParams["range"];

                if (range.Count != 1)
                {
                    throw new ArgumentOutOfRangeException("range","Bad range parameter number");
                }
                var nombres = range[0].Split("-");

                if (nombres.Length != 2
                    || !int.TryParse(nombres[0], out premier)
                    || !int.TryParse(nombres[1], out dernier))
                {
                    throw new ArgumentOutOfRangeException("range","Misformatted range");
                }

                if (dernier >= entityCOunt)
                {
                    dernier = entityCOunt;
                }

                if ( premier < 1 || dernier < premier || (dernier - premier) >= MaxPageSize )
                {
                    throw new ArgumentOutOfRangeException("range","Bad range request bound");
                }
                if( premier >= MaxPageSize)
                {
                    premier = dernier + 1;
                }
            }
           
            responseHeader["Content-Range"] = $"{premier}-{dernier}/{entityCOunt}";
            responseHeader["Accept-Range"] = $"apero {MaxPageSize}";
            return entities.Skip(premier - 1).Take(Quantity(premier, dernier));
        }
        
    }
}
