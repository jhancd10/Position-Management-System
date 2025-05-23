﻿using PositionManagement.Domain.Interfaces;

namespace PositionManagement.Domain.Entities
{
    public class Recruiter : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null;
        public string Cellphone { get; set; } = null;
        public string Email { get; set; } = null;

        public virtual ICollection<Position> Positions { get; set; } = [];

        public Guid EntityId => Id;
    }
}
