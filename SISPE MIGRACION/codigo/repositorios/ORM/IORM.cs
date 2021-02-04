using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios.ORM
{
    internal interface  IORM
    {

        dynamic construir<clase>(clase obj,bool retornar);
    }
}
