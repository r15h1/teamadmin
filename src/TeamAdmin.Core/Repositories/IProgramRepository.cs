using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IProgramRepository
    {
        IEnumerable<Program> GetPrograms();

        Program GetProgram(int programId);
    }
}