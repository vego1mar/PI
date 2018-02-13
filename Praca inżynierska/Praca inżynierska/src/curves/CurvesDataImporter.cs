using PI.src.general;
using PI.src.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;

namespace PI.src.curves
{
    public class CurvesDataImporter : IDisposable
    {
        private string fileName;
        private IList<IList<string>> dataset;
        public IList<double> Arguments { get; private set; }
        public IList<IList<double>> Values { get; private set; }
        public int CurvesNo { get; private set; }

        public CurvesDataImporter( string fullName )
        {
            fileName = fullName;
            dataset = Lists.Get( 0, 0, string.Empty );
            CurvesNo = 0;
        }

        /// <exception cref="FileNotFoundException">When resolved file path is invalid.</exception>
        /// <exception cref="PathTooLongException">When resolved file path exceeds the system-defined maximum length.</exception>
        /// <exception cref="IOException">When an input/output error occurs.</exception>
        /// <exception cref="OutOfMemoryException">When exceeds limit of used memory.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When trying to use non-existing index.</exception>
        public void Import()
        {
            Read();
            Parse();
            CurvesNo = Values[0].Count;
        }

        private void Read()
        {
            const int BUFFER_SIZE = 128;
            string line;
            string[] numbers;

            using ( var mappedFile = MemoryMappedFile.CreateFromFile( fileName ) ) {
                Stream mappedStream = mappedFile.CreateViewStream();

                using ( StreamReader reader = new StreamReader( mappedStream, Encoding.UTF8, true, BUFFER_SIZE ) ) {
                    while ( !reader.EndOfStream ) {
                        line = reader.ReadLine();
                        numbers = line.Split();
                        dataset.Add( numbers.ToList() );
                    }
                }
            }
        }

        private void Parse()
        {
            double? currentValue;
            Values = Lists.Get( dataset.Count, 0, 0.0 );
            Arguments = Lists.Get( dataset.Count, 0.0 );

            for ( int x = 0; x < dataset.Count; x++ ) {
                currentValue = Strings.TryGetValue( dataset[x][0], CultureInfo.InvariantCulture );
                Arguments[x] = currentValue ?? double.NaN;

                for ( int y = 1; y < dataset[x].Count; y++ ) {
                    currentValue = Strings.TryGetValue( dataset[x][y], CultureInfo.InvariantCulture );
                    Values[x].Add( currentValue ?? double.NaN );
                }
            }
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool isDisposingNeeded )
        {
            if ( isDisposingNeeded ) {
                fileName = null;
                dataset = null;
                Arguments = null;
                Values = null;
            }
        }
    }
}
