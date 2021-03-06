﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LOB.UI.Contract.Command;

#endregion

namespace LOB.UI.Core.Infrastructure {
    [Obsolete("Use events now")]
    public class CommandService : ICommandService {
        private static readonly Lazy<ICommandService> Lazy = new Lazy<ICommandService>(() => new CommandService());

        private readonly IDictionary<object, IList<ICommand>> _commands;

        private CommandService() { _commands = new Dictionary<object, IList<ICommand>>(); }

        public static ICommandService Default {
            get { return Lazy.Value; }
        }

        public void Register<T>(T token, ICommand command) {
            lock(_commands) {
                if(_commands.ContainsKey(token)) _commands[token].Add(command);
                else _commands.Add(token, new List<ICommand> {command});
            }
        }

        public void Execute<T>(T token, object arg) { foreach(var command in _commands[token].ToList()) command.Execute(arg); }

        public IEnumerable<ICommand> this[string token] {
            get { return _commands[token]; }
        }
    }
}