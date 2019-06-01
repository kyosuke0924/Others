
namespace CreateBlogContents
{

    [System.Xml.Serialization.XmlRoot("problem")]
    public class Probrem
    {
        [System.Xml.Serialization.XmlRoot("status")]
        public class Status
        {
            public int submission { get; set; }
            public int accepted { get; set; }
        }

        public string id { get; set; }
        public string name { get; set; }
        public int available { get; set; }
        public int problemtimelimit { get; set; }
        public int problemmemorylimit { get; set; }
        public Status status { get; set; }
    }

    public class Probrem2
    {
        public string html { get; set; }
    }

    public class TestcaseHeader
    {
        public class Info
        {
            public int Serial { get; set; }
            public string Name { get; set; }
            public int InputSize { get; set; }
            public int OutputSize { get; set; }
            public int Score { get; set; }
        }
        public string ProblemId { get; set; }
        public Info[] Headers { get; set; }
    }

    public class Testcase
    {
        public string ProblemId { get; set; }
        public int Serial { get; set; }
        public string @In { get; set; }
        public string @Out { get; set; }
    }

    public class Sample
    {
        public string ProblemId { get; set; }
        public int Serial { get; set; }
        public string @In { get; set; }
        public string @Out { get; set; }
    }





}
