using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class IAMonstro : MonoBehaviour
{
    public Transform alvo;

    public float velocidadePatrulha = 2f;
    public float velocidadePersseguicao = 3f;
    public float distanciaProxWaypoint = 3f;
    public bool jogadorEmVisao;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private bool espreitarJogadorExecutando; // Se a corotina UpdatePath está sendo executada

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < paiPosicoesPatrulha.childCount; i++)
        {
            posicoesPatrulha.Add(paiPosicoesPatrulha.GetChild(i).position);
        }

        indiceDestinoAtual = SortearProximoDestino(indiceDestinoAtual);
        seeker.StartPath(rb.position, posicoesPatrulha[indiceDestinoAtual], OnPathComplete);
    }

    public void MudarEstadoJogadorAvistado(bool avistado)
    {
        jogadorEmVisao = avistado;
        if (avistado)
        {
            Debug.Log(espreitarJogadorExecutando);
            if (espreitarJogadorExecutando)
            {
                espreitarJogadorExecutando = false;
                StopCoroutine(EspreitarJogador());
            }
        }
        else
        {
            // Se a corotina já estiver sendo executada, não iniciar outra
            if (!espreitarJogadorExecutando)
            {
                StartCoroutine(EspreitarJogador());
            }
        }
    }

    // Recalcula o caminho para o destino a cada 0.5 segundo
    IEnumerator EspreitarJogador()
    {
        espreitarJogadorExecutando = true;
        while(true)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, alvo.position, OnPathComplete);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        } 
    }

    // Sorteia o próximo destino que o monstro irá se dirigir para
    // Se sortear o mesmo que o anterior, repete o sorteio
    public int SortearProximoDestino(int indiceAtual)
    {
        int proximoDestino;
        do
        {
            proximoDestino = Random.Range(0, posicoesPatrulha.Count);
        } while (proximoDestino == indiceAtual);
        return proximoDestino;
    }

    //================================== UPDATE ===========================================

    // Variáveis para tratar a rotação do monstro
    [SerializeField] private float tempoDeSuavizacaoRotacao = .12f;
    Vector3 velocidadeDaSuavizacaoRotacao;
    Vector3 rotacaoAtual;

    public Transform paiPosicoesPatrulha;
    private List<Vector3> posicoesPatrulha = new List<Vector3>(); // Posições pelas quais o monstro vai ficar patrulhando
    private int indiceDestinoAtual;
    void Update()
    {
        // Seguir o jogador
        if (jogadorEmVisao)
        {
            Vector3 dir = (alvo.position - transform.position).normalized;
            float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.MoveRotation(Quaternion.AngleAxis(angulo, Vector3.forward));

            rb.MovePosition(transform.position + dir * velocidadePersseguicao * Time.deltaTime);
        }
        // Patrulhar
        else
        {
            PatrulharAteDestino();
        }
    }

    // Patrulha até o destino atual
    public void PatrulharAteDestino()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            indiceDestinoAtual = SortearProximoDestino(indiceDestinoAtual);
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, posicoesPatrulha[indiceDestinoAtual], OnPathComplete);
            }
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        //Calcula a direção para onde o monstro vai patrulhar
        Vector2 direcao = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Faz a rotação do monstro de acordo com a direção que ele está seguindo
        rotacaoAtual = Vector3.SmoothDamp(rotacaoAtual, new Vector3(direcao.x, direcao.y),
        ref velocidadeDaSuavizacaoRotacao, tempoDeSuavizacaoRotacao);

        float angulo = Mathf.Atan2(rotacaoAtual.y, rotacaoAtual.x) * Mathf.Rad2Deg;
        rb.SetRotation(angulo);

        // Move em direção ao próximo waypoint 
        rb.MovePosition(rb.position + direcao * velocidadePatrulha * Time.deltaTime);

        // Calcula se já deve andar para o waypoint seguinte ou não
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < distanciaProxWaypoint)
        {
            currentWaypoint++;
        }
    }

}
