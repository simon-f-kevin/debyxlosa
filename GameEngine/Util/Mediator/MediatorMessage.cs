namespace GameEngine.Util.Mediator
{
    public struct MediatorMessage
    {
        public int _entityId1 { get; set; }
        public int _entityId2 { get; set; }

        public MediatorMessage(int id1, int id2)
        {
            _entityId1 = id1;
            _entityId2 = id2;
        }
    }
}