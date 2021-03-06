﻿using System;
using Learning.EventStore.Snapshotting;

namespace Learning.EventStore.Test.Mocks
{
    public class TestSnapshotAggregate : SnapshotAggregateRoot<TestSnapshotAggregateSnapshot>
    {
        public TestSnapshotAggregate()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool Restored { get; private set; }
        public bool Loaded { get; private set; }
        public int Number { get; private set; }

        protected override TestSnapshotAggregateSnapshot CreateSnapshot()
        {
            return new TestSnapshotAggregateSnapshot { Number = Number };
        }

        protected override void RestoreFromSnapshot(TestSnapshotAggregateSnapshot snapshot)
        {
            Number = snapshot.Number;
            Restored = true;
        }

        private void Apply(TestAggregateDidSomething e)
        {
            Loaded = true;
            Number++;
        }

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }
    }

    public class TestSnapshotAggregateSnapshot : Snapshot
    {
        public int Number { get; set; }
    }
}
