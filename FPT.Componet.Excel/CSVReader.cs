using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// This is an Reader for read CSV File
    /// By default, CSVReader use double quote character (") as quote and escape character, and trim space for each field.
    /// Default delimiter is comma (,).
    /// CSV File contains fields separated by delimiter.
    /// If one field contains special characters (delimiter, quote, escape, new line), it must be wrap up with Quote.
    /// In quoted field, quote character must be preceded by escape character. Escape character can precede any characters.
    /// </summary>
    /// <remarks>
    /// When use CSV reader, you can set other Delimiter, Quote, Escape character.
    /// </remarks>
    public class CSVReader
    {
        #region Fields
        private TextReader reader;
        private char escape;
        private char delimiter;
        private bool trimSpace;
        private char quote;
        private char[] buffer;
        private int bufferSize;
        private int bufferLength;
        private bool eof;
        private int bufferPos;
        private bool fieldEnded;
        private bool skipEmptyLine = false;
        #endregion Fields

        #region Constants
        public const char DEFAULT_ESCAPE = '"';
        public const char DEFAULT_QUOTE = '"';
        public const char DEFAULT_DELIMITER = ',';
        public const int RECORD_BUFFER_SIZE = 16;
        public const bool DEFAULT_TRIM_SPACE = true;
        public const int DEFAULT_BUFFER_SIZE = 64;
        #endregion Constants

        #region Properties
        public char Escape
        {
            get { return escape; }
            set { escape = value; }
        }
        public char Delimiter
        {
            get { return delimiter; }
            set { delimiter = value; }
        }
        public char Quote
        {
            get { return quote; }
            set { quote = value; }
        }
        public bool TrimSpace
        {
            get { return trimSpace; }
            set { trimSpace = value; }
        }
        #endregion Properties

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        /// <param name="quote">Quote character</param>
        /// <param name="escape">Escape character</param>
        /// <param name="delimiter">Delimiter character</param>
        /// <param name="trimSpace">Trim space for parsed field</param>
        /// <param name="bufferSize">Size of buffer for read from reader stream</param>
        public CSVReader(TextReader reader, char quote, char escape, char delimiter, bool trimSpace, int bufferSize)
        {
            this.reader = reader;
            this.quote = quote;
            this.escape = escape;
            this.delimiter = delimiter;
            this.trimSpace = trimSpace;
            this.bufferSize = bufferSize;
            this.buffer = new char[bufferSize];
            // Fetch buffer for the first time.
            buffer = new char[bufferSize];
            FetchBuffer();
        }

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        /// <param name="quote">Quote character</param>
        /// <param name="escape">Escape character</param>
        /// <param name="trimSpace">Trim space for parsed field</param>
        /// <param name="bufferSize">Size of buffer for read from reader stream</param>
        public CSVReader(TextReader reader, char quote, char escape, bool trimSpace, int bufferSize)
            : this(reader, quote, escape, DEFAULT_DELIMITER, trimSpace, bufferSize)
        {
        }

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        /// <param name="quote">Quote character</param>
        /// <param name="trimSpace">Trim space for parsed field</param>
        /// <param name="bufferSize">Size of buffer for read from reader stream</param>
        public CSVReader(TextReader reader, char quote, bool trimSpace, int bufferSize)
            : this(reader, quote, DEFAULT_ESCAPE, DEFAULT_DELIMITER, trimSpace, bufferSize)
        {
        }

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        /// <param name="trimSpace">Trim space for parsed field</param>
        /// <param name="bufferSize">Size of buffer for read from reader stream</param>
        public CSVReader(TextReader reader, bool trimSpace, int bufferSize)
            : this(reader, DEFAULT_QUOTE, DEFAULT_ESCAPE, DEFAULT_DELIMITER, trimSpace, bufferSize)
        {
        }

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        /// <param name="bufferSize">Size of buffer for read from reader stream</param>
        public CSVReader(TextReader reader, int bufferSize)
            : this(reader, DEFAULT_QUOTE, DEFAULT_ESCAPE, DEFAULT_DELIMITER, DEFAULT_TRIM_SPACE, bufferSize)
        {
        }

        /// <summary>
        /// Initialize CSVReader object
        /// </summary>
        /// <param name="reader">The input text reader stream</param>
        public CSVReader(TextReader reader)
            : this(reader, DEFAULT_QUOTE, DEFAULT_ESCAPE, DEFAULT_DELIMITER, DEFAULT_TRIM_SPACE, DEFAULT_BUFFER_SIZE)
        {
        }

        /// <summary>
        /// Get property indicate End Of File.
        /// </summary>
        public bool EOF
        {
            get
            {
                return eof;
            }
        }

        public bool IgnoreEmptyLine
        {
            get { return skipEmptyLine; }
            set { skipEmptyLine = value; }
        }

        /// <summary>
        /// Read the next record in the CSV Stream
        /// </summary>
        /// <returns></returns>
        public String[] ReadNextRecord()
        {
            IList<String> result = new List<String>();

            SkipEmptyLine();
            if (eof)
            {
                return null;
            }
            bool hasEndField = false;
            fieldEnded = false;
            while (!eof)
            {
                if (!SkipWhiteSpace())
                {
                    break;
                }
                
                String strField = ReadNextField();
                hasEndField = false;
                if (trimSpace)
                {
                    result.Add(strField.Trim());
                }
                else
                {
                    result.Add(strField);
                }
                if (!SkipWhiteSpace())
                {
                    break;
                }
                fieldEnded = (buffer[bufferPos] == '\n' || buffer[bufferPos] == '\r');
                if (fieldEnded)
                {
                    break;
                }
                if (buffer[bufferPos] == delimiter)
                {
                    IncreaseBufferPosition();
                    hasEndField = true;
                }
                if (skipEmptyLine)
                {
                    SkipEmptyLine();
                }
            }
            if (hasEndField)
            {
                result.Add(string.Empty);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Increase buffer position by 1 and fetch buffer if needed
        /// </summary>
        /// <returns>
        /// True if not reach end of file
        /// False: if reach EOF or can't read from source stream
        /// </returns>
        private bool IncreaseBufferPosition()
        {
            bufferPos++;
            if (bufferPos >= bufferLength)
            {
                return FetchBuffer();
            }
            return true;
        }

        /// <summary>
        /// Skip all white-space character, except the new line
        /// </summary>
        /// <returns>
        /// False if reach EOF
        /// True: otherwise
        /// </returns>
        private bool SkipWhiteSpace()
        {
            while (!eof)
            {
                if (!Char.IsWhiteSpace(buffer[bufferPos]) || buffer[bufferPos] == '\n' || buffer[bufferPos] == '\r')
                {
                    return true;
                }
                IncreaseBufferPosition();
            }
            return false;
        }

        /// <summary>
        /// Skip all white-space character, including the new line
        /// </summary>
        private void SkipEmptyLine()
        {
            while (!eof)
            {
                if (!Char.IsWhiteSpace(buffer[bufferPos]))
                {
                    break;
                }
                IncreaseBufferPosition();
            }
        }

        /// <summary>
        /// Read next field from CSV Stream 
        /// </summary>
        /// <returns></returns>
        private String ReadNextField()
        {
            IList<char> result = new List<char>();
            if (eof)
            {
                return String.Empty;
            }
            if (buffer[bufferPos] == quote)
            {
                return ReadNextQuoteField();
            }
            else
            {
                return ReadNoneQuoteField();
            }
        }

        /// <summary>
        /// Read none quoted field
        /// </summary>
        /// <returns></returns>
        private String ReadNoneQuoteField()
        {
            IList<char> result = new List<char>();
            if (eof)
            {
                return String.Empty;
            }
            while (buffer[bufferPos] != delimiter && buffer[bufferPos] != '\n' && buffer[bufferPos] != '\r' && !eof)
            {
                if (buffer[bufferPos] == escape || buffer[bufferPos] == quote)
                {
                    result = null;
                    break;
                }
                result.Add(buffer[bufferPos]);
                IncreaseBufferPosition();
            }
            if (result == null)
            {
                return String.Empty;
            }
            return new String(result.ToArray());
        }

        /// <summary>
        /// Read next quoted field from CSV stream
        /// </summary>
        /// <returns></returns>
        private String ReadNextQuoteField()
        {
            // Skip open quote
            if (eof || !IncreaseBufferPosition())
            {
                return String.Empty;
            }
            IList<char> result = new List<char>();
            bool isEscaped = false;
            while (!eof)
            {
                // Contain quote in field
                if (isEscaped)
                {
                    isEscaped = false;
                    if (escape == quote && buffer[bufferPos] != quote)
                    {
                        break;
                    }
                    else
                    {
                        result.Add(buffer[bufferPos]);
                    }
                }
                else
                {
                    if (buffer[bufferPos] == escape)
                    {
                        isEscaped = true;
                    }
                    else if (buffer[bufferPos] == quote)
                    {
                        IncreaseBufferPosition();
                        //result = null;
                        break;
                    }
                    else
                    {
                        result.Add(buffer[bufferPos]);
                    }
                }
                if (!IncreaseBufferPosition())
                {
                    result = null;
                    break;
                }
            }
            if (result == null)
            {
                return String.Empty;
            }
            return new String(result.ToArray());
        }

        /// <summary>
        /// Fetch data from TextReader to buffer. Reset bufferPos
        /// </summary>
        /// <returns>
        /// True: If not EOF. Data is fetched to buffer, update bufferLength to the data length in buffer
        /// False: Otherwise, update eof=true
        /// </returns>
        private bool FetchBuffer()
        {
            if (eof)
            {
                return false;
            }
            else
            {
                Array.Clear(buffer, 0, bufferSize);
                bufferLength = reader.Read(buffer, 0, bufferSize);
                bufferPos = 0;
                if (bufferLength > 0)
                {
                    return true;
                }
                else
                {
                    eof = true;
                    Array.Clear(buffer, 0, bufferSize);
                    return false;
                }
            }
        }
    }
}
