using System;
using System.Collections;
using System.Collections.Generic;

namespace Dijkstra_s_algorithm
{
    class Program
    {
        static void Main()
        {
            // Хеш, представляющий взаимосвязи узлов и их стоимости.
            Hashtable graph = new Hashtable();

            // Хеши, представляющий узлы в графе.
            Hashtable start = new Hashtable();
            Hashtable a = new Hashtable();
            Hashtable b = new Hashtable();
            Hashtable final = new Hashtable();

            graph.Add("start", start);
            start.Add("a", 6);
            start.Add("b", 2);

            graph.Add("a", a);
            a.Add("fin", 1);

            graph.Add("b", b);
            b.Add("a", 3);
            b.Add("fin", 5);

            graph.Add("fin", final);


            // Хеш, представляющий стоимости каждого узла.
            Hashtable costs = new Hashtable();

            // По умолчанию стоимость конечного элемента
            // считается бесконечной.
            double infinity = double.PositiveInfinity;
            costs.Add("a", 6);
            costs.Add("b", 2);
            costs.Add("fin", infinity);


            // Хеш, представляющий родителей каждого узла.
            Hashtable parents = new Hashtable();
            parents.Add("a", "start");
            parents.Add("b", "start");
            parents.Add("fin", null);

            // Массив для проверенных узлов.
            List<string> processed = new List<string>();

            Console.WriteLine("Стоимость кратчайшего пути по алгоритму Дейкстры: "
                              + FindPathInGraph(graph, costs, parents, processed));

            Console.ReadLine();
        }

        /// <summary>
        /// Метод, представляющий алгоритм Дейкстры.
        /// </summary>
        /// <returns>
        /// Кратчайший путь от начального до конечного элементов.
        /// </returns>
        static int FindPathInGraph(Hashtable graph, Hashtable costs,
                                   Hashtable parents,
                                   List<string> processed)
        {
            // Получить узел с наименьшей стоимостью у начального
            // элемента.
            string node = GetLowestCostNode(costs, processed);

            // Найти минимальные стоимости всех узлов.
            while (node != null)
            {
                // Получить цену полученного узла.
                int cost = (int)costs[node];
                // Получить соседей узла в виде хеша.
                Hashtable neighbors = (Hashtable)graph[node];

                foreach (DictionaryEntry neighbor in neighbors)
                {
                    // Сформировать стоимость нового пути до соседа.
                    int newCost = cost + (int)neighbors[neighbor.Key];

                    // Если стоимость является бесконечностью, то 
                    // заменить её на новую стоимость.
                    if (Convert.ToDouble(costs[neighbor.Key]) == double.PositiveInfinity)
                    {
                        costs[neighbor.Key] = newCost;
                        parents[neighbor.Key] = node;
                    }
                    // В противном случае просто заменить старую стоимость
                    // на новую.
                    else if (newCost < Convert.ToInt32(costs[neighbor.Key]))
                    {
                        costs[neighbor.Key] = newCost;
                        parents[neighbor.Key] = node;
                    }
                }
                // Добавить узел в обработанные.
                processed.Add(node);
                // Взять следующий необработанный узел.
                node = GetLowestCostNode(costs, processed);
            }

            return (int)costs["fin"];
        }

        /// <summary>
        /// Метод, возвращающий узел с наименьшей стоимостью.
        /// </summary>
        /// <returns>
        /// Узел с наименьшей стоимостью из необработанных.
        /// </returns>
        static string GetLowestCostNode(Hashtable costs, List<string> progessed)
        {
            // Самая низкая стоимость среди всех.
            double lowestCost = double.PositiveInfinity;
            // Возвращаемый узел с наименьшей стоимостью.
            string lowestCostNode = null;

            foreach (DictionaryEntry node in costs)
            {
                double cost = Convert.ToDouble(costs[(string)node.Key]);

                if (cost < lowestCost && !progessed.Contains((string)node.Key))
                {
                    lowestCost = cost;
                    lowestCostNode = (string)node.Key;
                }
            }

            return lowestCostNode;
        }
    }
}
