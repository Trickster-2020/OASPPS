using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IRegisterService
    {
        public Task<int> check(String email);
        public Task post(String email,int test);
    }
}
