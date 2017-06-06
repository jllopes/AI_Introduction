using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Options;

public class BatchmodeConfig {

	public static bool batchmode = false;

	private static bool processed = false;
	private static readonly object syncLock = new object();

	public static void HandleArgs(EvolutionState engine, EvolvingSpaceShooter game){

		lock (syncLock) 
		{
			if (!processed) {
				// get the list of arguments 
				string[] args = Environment.GetCommandLineArgs ();

				bool show_help = false;

				OptionSet parser = new OptionSet () {
					"Usage: ",
					"",
					{"batchmode", "run in batchmode",
						v => batchmode = v != null
					},
					{"random=", "random generator number",
						(int v) => engine.random = v
					},
					{"level=", "the number of the {Level} to use.",
						(int v) => game.levelNumber = v
					},
					{"generations=", "the number of generations to execute.",
						(int v) => engine.numGenerations = v
					},
					{"probm=", "the mutation probability.",
						(float v) => engine.mutationProbability = v
					},
					{"probc=", "the crossover probability.",
						(float v) => engine.crossoverProbability = v
					},
					{"log=", "the logger output filename to use.",
						v => engine.statsFilename = v
					},
					{"tsize=", "the tournament size to use.",
						(int v) => engine.tournamentSize = v
					},
					{"elitism=", "the elitism number to use.",
						(int v) => engine.elitismAffected = v
					},
					{"seed=", "the seed to use.",
						(int v) => engine.random = v
					},
					{"cuts=", "Number of cuts for selection.",
						(int v) => engine.ncortes = v
					},
					{"tournament=","true=Tournament || false=Random",
						(bool v) => engine.flagSelection = v
							
						
					},

					{ "h|help",  "show this message and exit", 
						v => show_help = v != null 
					},
				};

				try{
					parser.Parse(args);
					processed = true;
					Console.WriteLine(engine.statsFilename);
					Debug.Log(engine.statsFilename);
				}
				catch (OptionException e) {
					Console.Write ("sokoban: ");
					Console.WriteLine (e.Message);
					Console.WriteLine ("Try `sokoban --help' for more information.");
					Application.Quit ();
					return;
				}

				if (show_help) {
					parser.WriteOptionDescriptions (Console.Out);
					Application.Quit();
					return;
				}

			}
		}

	}
}
