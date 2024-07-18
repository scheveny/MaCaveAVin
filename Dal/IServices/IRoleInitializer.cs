using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IServices
{
    public interface IRoleInitializer
    {
        Task InitializeRoles();
    }
}
