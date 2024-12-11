using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public int linha = 5;
    public int coluna = 5;


    [SerializeField] GameObject blocoPrefab; // Prefab do bloco

    [SerializeField] GameObject jogador1; // Prefab do jogador 1

    [SerializeField] GameObject jogador2; // Prefab do jogador 2

    [SerializeField] int linhas = 5; // N�mero de linhas da matriz

    [SerializeField] int colunas = 5; // N�mero de colunas da matriz

    [SerializeField] float espacamento = 1.1f; // Espa�amento entre os blocos

    private Bloco[,] grade; // Matriz 2D para armazenar os blocos

    private int territoriosConquistados;

    [SerializeField] private GameObject prefabDoBloco;

    private GameObject[,] blocos;

    #region Singleton
    public static GameManager instance;



    public void Awake()

    {

        instance = this;

        grade = new Bloco[linhas, colunas];

        CriarGrade();

    }


    #endregion





    public void ConquistarTerritorio(int x, int y, int jogadorID)
    {
        if (x >= 0 && x < linha && y >= 0 && y < coluna)
        {
            GameObject bloco = blocos[x, y];

            if (bloco != null)
            {
                // Atribua o bloco ao jogador
                bloco.GetComponent<Bloco>().Conquistar(jogadorID);
            }
        }
    }




    // M�todo para criar a matriz de blocos

    void CriarGrade()

    {

        for (int linha = 0; linha < linhas; linha++)

        {

            for (int coluna = 0; coluna < colunas; coluna++)

            {

                Vector2 posicao = new Vector2(coluna * espacamento, linha * espacamento);

                GameObject novoBloco = Instantiate(blocoPrefab, posicao, Quaternion.identity);

                grade[linha, coluna] = novoBloco.GetComponent<Bloco>();

            }

        }

        // Posicionar o jogador 1 e jogador 2

        Vector2 posicaoInicialJogador1 = new Vector2((colunas - 1) * espacamento / 2f - espacamento, (linhas - 1) * espacamento / 2f);

        Vector2 posicaoInicialJogador2 = new Vector2((colunas - 1) * espacamento / 2f + espacamento, (linhas - 1) * espacamento / 2f);

        Camera.main.transform.position = new Vector3((colunas - 1) * espacamento / 2f, (linhas - 1) * espacamento / 2f, -10);

        Camera.main.orthographicSize = linhas / 2f * espacamento;

        Instantiate(jogador1, posicaoInicialJogador1, Quaternion.identity);

        Instantiate(jogador2, posicaoInicialJogador2, Quaternion.identity);

    }

    // M�todo que � chamado quando um territ�rio � conquistado

    public void ConquistarTerritorio()

    {

        territoriosConquistados++;

        if (territoriosConquistados == grade.Length)

        {

            int jogador1 = 0;

            int jogador2 = 0;

            foreach (Bloco bloco in grade)

            {

                if (bloco.PegarJogadorDono() == 1)

                {

                    jogador1++;

                }

                else

                {

                    jogador2++;

                }

            }

            FimDeJogo(jogador1, jogador2);

        }

    }

    // M�todo que finaliza o jogo e declara o vencedor

    void FimDeJogo(int territoriosJogador1, int territoriosJogador2)

    {

        string vencedor;

        if (territoriosJogador1 > territoriosJogador2)

        {

            vencedor = "Jogador 1 venceu!";

        }

        else if (territoriosJogador2 > territoriosJogador1)

        {

            vencedor = "Jogador 2 venceu!";

        }

        else

        {

            vencedor = "Empate!";

        }

        Debug.Log("Fim do jogo! " + vencedor);

    }

}


