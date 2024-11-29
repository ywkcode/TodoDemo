namespace Todo.Entity
{
    public class DutyPlan : BaseEntity
    {
        public string PlanDate { get; set; }
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string Leader { get; set; }
        public string LeaderTel { get; set; }
        public string Dutyer { get; set; }
        public string DutyerTel { get; set; }
        public string? Remark { get; set; }


    }
}
