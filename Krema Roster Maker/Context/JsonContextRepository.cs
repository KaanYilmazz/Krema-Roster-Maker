using Krema_Roster_Maker.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krema_Roster_Maker.Context
{
    public class JsonContextRepository
    {
        private readonly string _filePath;
        private JsonContext _context;

        public JsonContextRepository(string filePath)
        {
            _filePath = filePath;
            _context = LoadFromFile();
        }

        public JsonContext Context => _context;

        public void Save()
        {
            JsonSerializerHelper.WriteToFile(_context, _filePath);
        }

        private JsonContext LoadFromFile()
        {
            if (File.Exists(_filePath))
            {
                return JsonSerializerHelper.ReadFromFile<JsonContext>(_filePath);
            }
            return new JsonContext();
        }
    }

}
