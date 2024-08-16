﻿namespace EpidemiologicalTrackingApi.Exceptions
{
    public class IndividualNotFoundException : Exception
    {
        public IndividualNotFoundException(int id) : base($"Individual with ID {id} not found.")
        {
        }
    }
}
