using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Utilities.Interfaces
{
    /// <summary>
    /// Interface for file reader.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reads all text from the specified file.
        /// </summary>
        /// <param name="filePath">The full file path to be read.</param>
        /// <returns>String containing file text.</returns>
        string ReadAllText(string filePath);
    }
}
