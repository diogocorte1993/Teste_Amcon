using Newtonsoft.Json;
using Questao2.Model;

public class Program
{
    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    public static async Task MainAsync(string[] args)
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static string GetRoute(string teamMode, string team, int year, int page) => $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamMode}={team}&page={page}";

    private async static Task<ApiResult> GetResult(HttpClient httpClient, string teamMode, string team, int year, int page)
    {
        using (HttpResponseMessage response = await httpClient.GetAsync(GetRoute(teamMode, team, year, page)))
        {
            return JsonConvert.DeserializeObject<ApiResult>(await response.Content.ReadAsStringAsync());
        }
    }

    public async static Task<int> getTotalScoredGoals(string team, int year)
    {
        string[] teamModes = { "team1", "team2" };
        HttpClient httpClient = new();

        int totalGols = 0;

        foreach (string teamMode in teamModes)
        {
            int totalPage = 1;
            int currentPage = 0;
            string golsProperty = $"{teamMode}goals";
            do
            {
                currentPage++;

                var apiResult = await GetResult(httpClient, teamMode, team, year, currentPage);
                totalPage = apiResult.total_pages;                

                foreach (Partida partida in apiResult.data)
                {
                    int golsNaPartida = int.Parse(partida.GetType().GetProperty(golsProperty).GetValue(partida).ToString());
                    totalGols += golsNaPartida;
                }

            } while (currentPage < totalPage);
        }

        return totalGols;
    }



}