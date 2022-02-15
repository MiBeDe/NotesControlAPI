using Microsoft.EntityFrameworkCore;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly NotesControlContext _context;

        public ConfigurationRepository(NotesControlContext context)
        {
            _context = context;
        }

        public ConfigurationModel getConfiguration(int userId)
        {
            var config = _context.Configuration.Where(x => x.UserId == userId).FirstOrDefault();

            return config;
        }

        public ConfigurationModel updateConfiguration(ConfigurationModel configuration, int userId)
        {
            var config = _context.Configuration.AsNoTracking().Where(x => x.UserId == userId).FirstOrDefault();

            configuration.Id = config.Id;
            configuration.UserId = config.UserId;

            _context.Configuration.Update(configuration);
            _context.SaveChanges();

            return configuration;
        }
    }
}
