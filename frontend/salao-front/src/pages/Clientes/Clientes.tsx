import { useEffect, useState } from "react";
import {
  listarClientes,
  criarCliente,
  deletarCliente,
  atualizarCliente,
} from "../../services/clienteService";

import { Pencil, Trash, Plus } from "lucide-react";

import "./Clientes.css";
import MainLayout from "../../layouts/mainLayout/MainLayout";

export default function Clientes() {
  const [clientes, setClientes] = useState<any[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [cliente, setCliente] = useState<any>({
    nome: "",
    telefone: "",
  });

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const data = await listarClientes();
    setClientes(data);
  };

  // 🔥 ABRIR CRIAÇÃO
  const abrirCriar = () => {
    setCliente({ nome: "", telefone: "" });
    setModalOpen(true);
  };

  // 🔥 ABRIR EDIÇÃO
  const abrirEditar = (c: any) => {
    setCliente(c);
    setModalOpen(true);
  };

  // 💾 SALVAR (CREATE OU UPDATE)
  const salvar = async () => {
    if (cliente.id) {
      await atualizarCliente(cliente.id, cliente);
    } else {
      await criarCliente(cliente);
    }

    setModalOpen(false);
    setCliente({ nome: "", telefone: "" });

    load();
  };

  // 🗑 DELETE
  const remover = async (id: number) => {
    await deletarCliente(id);
    load();
  };

  return (
    <MainLayout>
      <div className="container">
        <div className="page-header">
          <h1>Clientes</h1>

          <button className="btn-primary new-client-btn" onClick={abrirCriar}>
            <Plus size={18} />
            Novo Cliente
          </button>
        </div>

        {/* TABELA */}
        <div className="table-wrapper">
          <table>
            <thead>
              <tr>
                <th>Nome</th>
                <th>Telefone</th>
                <th>Ações</th>
              </tr>
            </thead>

            <tbody>
              {clientes.map((c) => (
                <tr key={c.id}>
                  <td>{c.nome}</td>
                  <td>{c.telefone}</td>

                  <td>
                    <div className="actions">
                      <button
                        className="edit"
                        onClick={() => abrirEditar(c)}
                      >
                        <Pencil size={16} />
                      </button>

                      <button
                        className="delete"
                        onClick={() => remover(c.id)}
                      >
                        <Trash size={16} />
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>  

        {/* MODAL ÚNICO */}
        {modalOpen && (
          <div className="modal-backdrop">
            <div className="modal">
              <h2>
                {cliente.id ? "Editar Cliente" : "Novo Cliente"}
              </h2>

              <input
                placeholder="Nome"
                value={cliente.nome}
                onChange={(e) =>
                  setCliente({ ...cliente, nome: e.target.value })
                }
              />

              <input
                placeholder="Telefone"
                value={cliente.telefone}
                onChange={(e) =>
                  setCliente({ ...cliente, telefone: e.target.value })
                }
              />

              <div className="modal-actions">
                <button onClick={() => setModalOpen(false)}>
                  Cancelar
                </button>

                <button onClick={salvar} className="save">
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