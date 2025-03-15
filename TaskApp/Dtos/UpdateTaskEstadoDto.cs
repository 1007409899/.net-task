using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Enums;

namespace TaskApp.Dtos
{
    public class UpdateTaskEstadoDto
    {
        public StateTask  State { get; set; }
    }
}
