using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class NeighbourStrategyFactory
    {
        Dictionary<string, Type> strategies;

        public NeighbourStrategyFactory()
        {
            LoadTypesIFindNeighbourStrategy();
        }

		private void LoadTypesIFindNeighbourStrategy()
		{
			strategies = new Dictionary<string, Type>();
			Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

			foreach (var type in typesInThisAssembly.Where(type => type.GetInterface(typeof(IFindNeighbourStrategy).ToString()) != null))
				strategies.Add(type.Name.ToLower(), type);
		}

		internal IFindNeighbourStrategy CreateInstance(string nameOfStrategy)
		{
			Type t = GetTypeToCreate(nameOfStrategy, strategies) ?? GetTypeToCreate("more", strategies);
			return Activator.CreateInstance(t) as IFindNeighbourStrategy;
		}

		private Type GetTypeToCreate(string nameOfStrategy, Dictionary<string, Type> strategies)
			=> strategies.FirstOrDefault(possibleStrategy => possibleStrategy.Key.Contains(nameOfStrategy)).Value;
    }

}
