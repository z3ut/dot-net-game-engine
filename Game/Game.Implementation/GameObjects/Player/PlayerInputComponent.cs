using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using Game.Implementation.GameObjects.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Implementation.GameObjects.Player
{
    public class PlayerInputComponent : IGameComponent
    {
        private static bool _isDestroyed = false;

        private static ConsoleKey? _lastMovementKey;

        private static readonly IEnumerable<ConsoleKey> _movementKeys = new List<ConsoleKey>()
        {
            ConsoleKey.W,
            ConsoleKey.A,
            ConsoleKey.S,
            ConsoleKey.D
        };

        private static ConsoleKey? _lastFireKey;

        private static readonly IEnumerable<ConsoleKey> _fireKeys = new List<ConsoleKey>()
        {
            ConsoleKey.NumPad8,
            ConsoleKey.NumPad6,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad4
        };

        public object Clone()
        {
            return new PlayerInputComponent();
        }

        public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
        {
            _isDestroyed = true;
        }

        public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
        {
        }

        public void Init(IGameObject gameObject, IGameWorld gameWorld)
        {
            var task = new Task(InterceptKeyboardInput);
            task.Start();
        }

        public void Update(IGameObject gameObject, IGameWorld gameWorld)
        {
            if (_lastMovementKey != null)
            {
                float direction = MovementKeyToDegree(_lastMovementKey.Value);

                gameObject.HandleEvent(gameWorld, new MoveEvent(direction));

                _lastMovementKey = null;
            }

            if (_lastFireKey != null)
            {
                float direction = FireKeyToDegree(_lastFireKey.Value);

                gameObject.HandleEvent(gameWorld, new FireEvent(direction));

                _lastFireKey = null;
            }
        }

        private static void InterceptKeyboardInput()
        {
            while (!_isDestroyed)
            {
                var key = Console.ReadKey(true).Key;

                if (IsMovementKey(key))
                {
                    _lastMovementKey = key;
                }

                if (IsFireKey(key))
                {
                    _lastFireKey = key;
                }
            }
        }

        private static bool IsMovementKey(ConsoleKey key)
        {
            return _movementKeys.Contains(key);
        }

        private static bool IsFireKey(ConsoleKey key)
        {
            return _fireKeys.Contains(key);
        }

        private static float MovementKeyToDegree(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.W => 0,
                ConsoleKey.A => 270,
                ConsoleKey.S => 180,
                ConsoleKey.D => 90,
                _ => throw new ArgumentException("Key is not movement key"),
            };
        }

        private static float FireKeyToDegree(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.NumPad8 => 0,
                ConsoleKey.NumPad6 => 90,
                ConsoleKey.NumPad2 => 180,
                ConsoleKey.NumPad4 => 270,
                _ => throw new ArgumentException("Key is not fire key"),
            };
        }
    }
}
