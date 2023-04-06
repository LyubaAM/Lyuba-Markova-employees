
namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Pair of employees worked together on a project.
    /// </summary>
    public class CoWorkers
    {
        private int empId1;
        private int empId2;
        private int projectId;
        private int daysWorked;

        public int EmpId1
        {
            get { return empId1; }
            set { empId1 = value; }
        }
        public int EmpId2
        {
            get { return empId2; }
            set { empId2 = value; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }
        public int DaysWorked
        {
            get { return daysWorked; }
            set { daysWorked = value; }
        }
    }
}
