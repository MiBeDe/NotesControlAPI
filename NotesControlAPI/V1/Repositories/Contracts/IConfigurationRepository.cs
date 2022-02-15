using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface IConfigurationRepository
    {
        ConfigurationModel updateConfiguration(ConfigurationModel configuration, int userId);
        ConfigurationModel getConfiguration(int userId);
    }
}
