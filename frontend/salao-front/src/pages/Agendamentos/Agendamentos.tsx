import { useEffect, useState } from "react";
import MainLayout from "../../layouts/mainLayout/MainLayout";

import {
  listarAgendamentos,
  criarAgendamento,
  atualizarAgendamento,
  deletarAgendamento,
  listarMeusAgendamentos
} from "../../services/agendamentoService";

import { listarClientes } from "../../services/clienteService";
import { listarServicos } from "../../services/servicoService";

import { Pencil, Trash, Plus } from "lucide-react";
import { getUser } from "../../services/userStorage";

import "./Agendamentos.css";

export default function Agendamentos() {
  const [agendamentos, setAgendamentos] = useState<any[]>([]);
  const [clientes, setClientes] = useState<any[]>([]);
  const [servicos, setServicos] = useState<any[]>([]);
  const [modalOpen, setModalOpen] = useState(false);

  const user = getUser();

  const [agendamento, setAgendamento] = useState<any>({
    clienteId: "",
    servicosIds: [],
    dataHora: "",
    status: 0,
  });

  useEffect(() => {
    load();
    loadExtras();
  }, []);

 
  const load = async () => {
  const user = getUser();

  let data = [];

  // 👨‍💼 ADMIN
  if (user?.tipo === 1) {
    data = await listarAgendamentos();
  }

  // 👤 CLIENTE
  else {
    data = await listarMeusAgendamentos(user.id);
  }

  setAgendamentos(data);
};

  const loadExtras = async () => {
    setClientes(await listarClientes());
    setServicos(await listarServicos());
  };

  const abrirCriar = () => {
    setAgendamento({
      clienteId: "",
      servicosIds: [],
      dataHora: "",
      status: 0,
    });

    setModalOpen(true);
  };

  const abrirEditar = (a: any) => {
    setAgendamento({
      id: a.id,
      clienteId: a.clienteId,
      servicosIds: a.servicosIds || [],
      dataHora: a.dataHora,
      status: a.status,
    });

    setModalOpen(true);
  };

  const salvar = async () => {
    const payload = {
      clienteId: agendamento.clienteId,
      servicosIds: agendamento.servicosIds,
      dataHora: agendamento.dataHora,
      status: agendamento.status,
      usuarioId: user.id
    };

    if (agendamento.id) {
      await atualizarAgendamento(agendamento.id, payload);
    } else {
      await criarAgendamento(payload);
    }

    setModalOpen(false);
    load();
  };

  const remover = async (id: number) => {
    await deletarAgendamento(id);
    load();
  };

  return (
    <MainLayout>
      <div className="container">

        <div className="page-header">
          <h1>Agendamentos</h1>

          <button className="new-btn" onClick={abrirCriar}>
            <Plus size={18} />
            Novo Agendamento
          </button>
        </div>

        {/* TABLE */}
        <table>
          <thead>
            <tr>
              <th>Cliente</th>
              <th>Serviços</th>
              <th>Total</th>
              <th>Data</th>
              <th>Status</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {agendamentos.map((a) => (
              <tr key={a.id}>
                <td>{a.cliente}</td>
                <td>{a.servicos?.join(", ")}</td>
                <td>R$ {a.total?.toFixed(2) || "0,00"}</td>
                <td>{new Date(a.dataHora).toLocaleString()}</td>

                <td>
                  {a.status === 0 && "Pendente"}
                  {a.status === 1 && "Confirmado"}
                  {a.status === 2 && "Cancelado"}
                </td>

                <td>
                  <div className="actions">

                    <button
                      className="edit"
                      onClick={() => abrirEditar(a)}
                    >
                      <Pencil size={16} />
                    </button>

                    <button
                      className="delete"
                      onClick={() => remover(a.id)}
                    >
                      <Trash size={16} />
                    </button>

                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        {/* MODAL */}
        {modalOpen && (
          <div className="modal-backdrop">
            <div className="modal">

              <h2>
                {agendamento.id ? "Editar Agendamento" : "Novo Agendamento"}
              </h2>

              {/* CLIENTE */}
              <select
                value={agendamento.clienteId}
                onChange={(e) =>
                  setAgendamento({
                    ...agendamento,
                    clienteId: Number(e.target.value),
                  })
                }
              >
                <option value="">Selecione cliente</option>
                {clientes.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.nome}
                  </option>
                ))}
              </select>

              {/* SERVIÇOS */}
              <select
                onChange={(e) => {
                  const id = Number(e.target.value);
                  if (!id) return;

                  const exists = agendamento.servicosIds.includes(id);

                  if (!exists) {
                    setAgendamento({
                      ...agendamento,
                      servicosIds: [...agendamento.servicosIds, id],
                    });
                  }
                }}
              >
                <option value="">Adicionar serviço</option>

                {servicos.map((s) => (
                  <option key={s.id} value={s.id}>
                    {s.nome}
                  </option>
                ))}
              </select>

              <div className="chips">
                {agendamento.servicosIds.map((id: number) => {
                  const servico = servicos.find((s) => s.id === id);

                  return (
                    <span key={id} className="chip">
                      {servico?.nome}

                      <button
                        onClick={() =>
                          setAgendamento({
                            ...agendamento,
                            servicosIds: agendamento.servicosIds.filter(
                              (s: number) => s !== id
                            ),
                          })
                        }
                      >
                        ✕
                      </button>
                    </span>
                  );
                })}
              </div>

              {/* DATA */}
              <input
                type="datetime-local"
                value={agendamento.dataHora}
                onChange={(e) =>
                  setAgendamento({
                    ...agendamento,
                    dataHora: e.target.value,
                  })
                }
              />

              {/* STATUS */}
              <select
                value={agendamento.status}
                onChange={(e) =>
                  setAgendamento({
                    ...agendamento,
                    status: Number(e.target.value),
                  })
                }
              >
                <option value={0}>Pendente</option>
                <option value={1}>Confirmado</option>
                <option value={2}>Cancelado</option>
              </select>

              <div className="modal-actions">
                <button onClick={() => setModalOpen(false)}>
                  Cancelar
                </button>

                <button className="save" onClick={salvar}>
                  Salvar
                </button>
              </div>

            </div>
          </div>
        )}

      </div>
    </MainLayout>
  );
}