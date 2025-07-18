using System;

namespace DDD.Core;
 /// <summary>
 /// Represents a domain event in the system.
 /// </summary>
 public abstract class DomainEvent
 {
     public DateTime OccurredOn { get; }
     protected DomainEvent()
     {
         OccurredOn = DateTime.UtcNow;
     }
 }
