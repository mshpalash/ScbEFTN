using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Text;
using System.Data;

namespace EFTN.BLL
{
	/// <summary>
	/// Base class for implementing text file based imports
	/// </summary>
	public class ImportDelimitedFile
	{
		#region Instance values

		private string _filter;

		#endregion

        public DataTable dtDelimitedData;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImportDelimitedFile"/> class.
		/// </summary>
		/// <remarks>The default delimiter is set to tabs.</remarks>
		public ImportDelimitedFile()
		{
		}
		#endregion

		/// <summary>
		/// Gets or sets the filter to be used when querying the delimited file.
		/// </summary>
		/// <value>The filter.</value>
		private string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
			}
		}

        //#endregion

		/// <summary>
		/// Importer the specified fileName.
		/// </summary>
		/// <param name="fileName">The fileName.</param>
        private bool Import(string filename,  string delimiter)
		{
            bool isValideFile = true;
            string[] columnNames = null;
            FileInfo file = new FileInfo(filename);

            //WriteSchemaIniFile(file);
            dtDelimitedData = new DataTable();
            //  Create the new table         
            dtDelimitedData.Locale = System.Globalization.CultureInfo.CurrentCulture;
            //  Check file        
            if (!File.Exists(filename))
                throw new FileNotFoundException("File not found", filename);
            //  Process the file line by line         
            string line;
            using (TextReader tr = new StreamReader(filename, Encoding.Default))
            {
                //  If column names were not passed, we'll read them from the file             
                if (columnNames == null)
                {
                    //  Get the first line                 
                    line = tr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        throw new IOException("Could not read column names from file.");

                    columnNames = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                }

                //  Add the columns to the data table             
                foreach (string colName in columnNames)
                    dtDelimitedData.Columns.Add(colName);
            }
            try
            {
                using (OleDbConnection con = JetConnection(file, delimiter))
                {
                    using (OleDbCommand cmd = JetCommand(file, con))
                    {
                        con.Open();

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.FieldCount != columnNames.Length)
                            {
                                return false;
                            }
                            while (reader.Read())
                            {
                                // Get the individual columns from the current reader position
                                object[] columns = new object[reader.FieldCount];

                                reader.GetValues(columns);
                                dtDelimitedData.Rows.Add(columns);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                isValideFile = false;
            }

            return isValideFile;
		}

		/// <summary>
		/// Build a Jet Engine connection string.
		/// </summary>
		/// <param name="file">The file object of the delimited file.</param>
		/// <returns>A valid connection string.</returns>
        private OleDbConnection JetConnection(FileInfo file, string customDelimeter)
        {
            StringBuilder connection = new StringBuilder("Provider=Microsoft.Jet.OLEDB.4.0");

            connection.AppendFormat(";Data Source=\"{0}\"", file.DirectoryName);
            connection.Append(";Extended Properties='text;HDR=Yes");


            if (customDelimeter == null)
            {
                throw new InvalidOperationException("Custom delimiter is not specified");
            }
            if (!customDelimeter.Equals(","))
            {
                connection.AppendFormat(";FMT=Delimited({0})", customDelimeter);
            }
            connection.Append("';");

            return new OleDbConnection(connection.ToString());
        }

		/// <summary>
		/// Jets the command.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="con">The con.</param>
		/// <returns></returns>
		private OleDbCommand JetCommand(FileInfo file, OleDbConnection con)
		{
			StringBuilder commandText = new StringBuilder("SELECT * FROM ");

			commandText.AppendFormat("[{0}]", file.Name);
			if (_filter != null)
			{
				commandText.Append(" WHERE ");
				commandText.Append(_filter);
			}


			OleDbCommand cmd = new OleDbCommand(commandText.ToString(), con);

			// Set the timeout to an extremely large value. Delimited files can be large, and 
			// executing the command might take up a lot of time
			cmd.CommandTimeout = 60000;

			return cmd;
		}
        
		public static DataTable ReadDelimitedFile(string fileName, string delimiter)
		{
			ImportDelimitedFile importer = new ImportDelimitedFile();

            DataTable dtEFT = new DataTable();
            if (importer.Import(fileName, delimiter))
            {
                return importer.dtDelimitedData;
            }
            return dtEFT;
		}
		/// <summary>
		/// Writes the schema ini file if it doesn't yet exist.
		/// </summary>
        ///// <param name="file">The file.</param>
        //private void WriteSchemaIniFile(FileInfo file)
        //{
        //    string schema = Path.Combine(file.DirectoryName, "Schema.ini");

        //    if (!File.Exists(schema))
        //    {
        //        using (StreamWriter writer = new StreamWriter(schema))
        //        {
        //            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "[{0}]", file.Name));

        //            switch (_delimiter)
        //            {
        //                case DelimiterType.CustomDelimited:
        //                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "Format=Delimited({0})", _customDelimiter));
        //                    break;
        //                case DelimiterType.CsvDelimited:
        //                case DelimiterType.TabDelimited:
        //                default:
        //                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "Format={0}", _delimiter));
        //                    break;
        //            }
        //        }
        //    }
        //}

	}
}
