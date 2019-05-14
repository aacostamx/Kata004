using ContosoUniversity.Models;

namespace ContosoUniversity.Infrastructure.Tags
{
    public class ExamSelectElementBuilder : EntitySelectElementBuilder<Exam>
    {
        protected override int GetValue(Exam instance)
        {
            return instance.Id;
        }

        protected override string GetDisplayValue(Exam instance)
        {
            return instance.Title;
        }
    }
}
