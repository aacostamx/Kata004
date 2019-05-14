using System.Linq;

namespace ContosoUniversity.IntegrationTests.Features.Exams
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Shouldly;
    using Xunit;
    using static SliceFixture;

    public class IndexTest : IntegrationTestBase
    {
        [Fact]
        public async Task Should_list_exams()
        {
            var exams = new Exam[]
            {
                new Exam { Title = "Diagnostic Testing", Date = new DateTime(2019, 5, 15, 15, 30, 0) },
                new Exam { Title = "Formative Assessments", Date = new DateTime(2019, 5, 16, 10, 0, 0) },
                new Exam { Title = "Benchmark Testing", Date = new DateTime(2019, 5, 17, 9, 30, 0) },
                new Exam { Title = "Summative Assessments", Date = new DateTime(2019, 5, 20, 15, 0, 0) }
            };

            var exam = new Exam { Title = "Diagnostic Testing", Date = new DateTime(2019, 5, 15, 15, 30, 0) };
            var exam2 = new Exam { Title = "Formative Assessments", Date = new DateTime(2019, 5, 16, 10, 0, 0) };

            await InsertAsync(exam, exam2);

            var query = new Pages.Exams.Index.Query();

            var result = await SendAsync(query);

            result.Exams.ShouldNotBeNull();
            result.Exams.Count.ShouldBeGreaterThanOrEqualTo(2);
            result.Exams.Select(m => m.Id).ShouldContain(exam.Id);
            result.Exams.Select(m => m.Id).ShouldContain(exam.Id);
        }
    }
}
