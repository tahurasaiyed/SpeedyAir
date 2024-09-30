using SpeedyAir.Utilities.Interfaces;

namespace SpeedyAir.Utilities
{
    /// <summary>
    /// Contains all operations associated with reading a file.
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Reads all text from the specified file.
        /// </summary>
        /// <param name="filePath">The full file path to be read.</param>
        /// <returns>String containing file text.</returns>
        public string ReadAllText(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}