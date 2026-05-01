import { useEffect, useState } from "react";
import {
  listarServicos,
  criarServico,
  atualizarServico,
  deletarServico,
} from "../../services/servicoService";

import { Pencil, Trash, Plus } from "lucide-react";
import MainLayout from "../../layouts/mainLayout/MainLayout";

import "./Servicos.css";

export default function Servicos() {
  const [servicos, setServicos] = useState<any[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [servico, setServico] = useState<any>({
    nome: "",
    preco: "",
  });

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const data = await listarServicos();
    setServicos(data);
  };

  // 🔥 criar modal
  const abrirCriar = () => {
    setServico({ nome: "", preco: "" });
    setModalOpen(true);
  };

  // 🔥 editar modal
  const abrirEditar = (s: any) => {
    setServico(s);
    setModalOpen(true);
  };

  // 💾 salvar create/update
  const salvar = async () => {
    if (servico.id) {
      await atualizarServico(servico.id, servico);
    } else {
      await criarServico(servico);
    }

    setModalOpen(false);
    load();
  };

  // 🗑 delete
  const remover = async (id: number) => {
    await deletarServico(id);
    load();
  };

  return (
    <MainLayout>
      <div className="container">

        <div className="page-header">
          <h1>Serviços</h1>

          <button className="btn-primary new-btn" onClick={abrirCriar}>
            <Plus size={18} />
            Novo Serviço
          </button>
        </div>

        {/* TABLE */}
        <table>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Preço</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {servicos.map((s) => (
              <tr key={s.id}>
                <td>{s.nome}</td>
                <td>R$ {s.preco}</td>

                <td>
                  <div className="actions">
                    <button className="edit" onClick={() => abrirEditar(s)}>
                      <Pencil size={16} />
                    </button>

                    <button className="delete" onClick={() => remover(s.id)}>
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
                {servico.id ? "Editar Serviço" : "Novo Serviço"}
              </h2>

              <input
                placeholder="Nome"
                value={servico.nome}
                onChange={(e) =>
                  setServico({ ...servico, nome: e.target.value })
                }
              />

              <input
                placeholder="Preço"
                value={servico.preco}
                onChange={(e) =>
                  setServico({ ...servico, preco: e.target.value })
                }
              />

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