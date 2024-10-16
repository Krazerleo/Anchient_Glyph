using System;

namespace AncientGlyph.GameScripts.Common
{
    public struct Result<TValue, TFailStatus> where TFailStatus : Enum
    {
        private readonly TValue _value;
        public TValue Value => GetValue();
        private bool _isFailed;
        /// <summary>
        /// Returns Fail Status Code. If result is set, then returns default value of fail status error
        /// </summary>
        public TFailStatus FailStatus { get; }
        /// <summary>
        /// Returns true if result is invalid 
        /// </summary>
        public bool IsFailed() => _isFailed;

        private TValue GetValue()
        {
            if (_isFailed)
            {
                throw new InvalidOperationException("Try to get value from invalid result");
            }

            return _value;
        }

        private Result(TValue value, TFailStatus failStatus)
        {
            _isFailed = false;
            _value = value;
            FailStatus = failStatus;
        }

        public static implicit operator Result<TValue, TFailStatus>(TFailStatus failStatus)
        {
            Result<TValue, TFailStatus> result = new(default, failStatus)
            {
                _isFailed = true,
            };

            return result;
        }

        public static implicit operator Result<TValue, TFailStatus>(TValue value)
        {
            Result<TValue, TFailStatus> result = new(value, default)
            {
                _isFailed = false,
            };

            return result;
        }
    }
}