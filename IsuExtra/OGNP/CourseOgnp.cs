using System.Collections.Generic;
using Isu.Services.Students;
using IsuExtra.Tools;

namespace IsuExtra.OGNP
{
    public class CourseOgnp
    {
        private readonly List<Stream> _streams = new List<Stream>();
        private char _megaFacultySymbol;
        private int _id;

        public CourseOgnp(char megaFacultySymbol)
        {
            _megaFacultySymbol = megaFacultySymbol;
            _id = OgnpIdGenerator.GetId();
        }

        public Stream AddStream(Stream stream)
        {
            _streams.Add(stream);
            return stream;
        }

        public void EnrollNewStudent(Student student, string streamName)
        {
            GetStreamByName(streamName).EnrollNewStudent(student);
        }

        public void RemoveStudent(Student student)
        {
            _streams.ForEach(stream =>
            {
                stream.GetGroup().RemoveStudent(student);
            });
        }

        public List<string> GetNamesOfStreams()
        {
            var result = new List<string>();
            _streams.ForEach(stream =>
            {
                result.Add(stream.GetName());
            });
            return result;
        }

        public Stream GetStreamByName(string name)
        {
            var stream = _streams.Find(stream => stream.GetName() == name);
            if (stream == null)
                throw new OgnpException($"There is no stream with name {name}");
            return stream;
        }

        public char GetMegaFacultySymbol() => _megaFacultySymbol;

        public int GetId() => _id;

        public bool ContainsStudent(int studentId)
        {
            var res = false;
            _streams.ForEach(stream =>
            {
                if (stream.ContainsStudent(studentId))
                    res = true;
            });
            return res;
        }
    }
}