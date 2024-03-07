// InventoryTable.tsx
import React from 'react';
import StatusBadge from '../../../components/Generic/StatusBadge';
import { useState } from 'react';

// Define TypeScript interface for inventory item
interface InventoryItem {
  id: number;
  producto: string;
  cantidad: number;
  categoria: string;
  precioUnitario: number;
  etiqueta: string;
  estado: string;
}

const InventoryTable: React.FC = () => {

  // State for the search term
  const [searchTerm, setSearchTerm] = useState('');

  // Sample data for the inventory
  const inventoryData: InventoryItem[] = [
    {
      id: 1,
      producto: 'Vasos',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'No Disponible'
    },
    {
      id: 1,
      producto: 'Mesas',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'Disponible'
    },
    {
      id: 1,
      producto: 'Sillas',
      cantidad: 10,
      categoria: 'Categoría A',
      precioUnitario: 100,
      etiqueta: 'Etiqueta 1',
      estado: 'Disponible'
    },
    // Add more inventory items here
  ];

  // Calculate precioTotal for each item and filter based on search term
  const enrichedData = inventoryData
    .map(item => ({
      ...item,
      precioTotal: item.cantidad * item.precioUnitario,
    }))
    .filter(item => {
      // Adjust this logic to search in multiple fields if necessary
      return (
        item.producto.toLowerCase().includes(searchTerm.toLowerCase())
      );
    });

  return (
    <div>
      <div className="search-bar">
        <input
          type="text"
          placeholder="Buscar"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="mt-1 mb-4 w-full border-2 border-gray-300 bg-white h-10 px-5 pr-16 rounded-lg text-sm focus:outline-none"
        />
      </div>
      <table className="min-w-full table-auto border">
        <thead >
          <tr>
            <th className="text-left px-4 py-2" > Producto </th>
            < th className="text-left px-4 py-2" > Cantidad </th>
            < th className="text-left px-4 py-2" > Categoría </th>
            < th className="text-left px-4 py-2" > Precio Unitario </th>
            < th className="text-left px-4 py-2" > Precio Total </th>
            < th className="text-left px-4 py-2" > Etiqueta </th>
            < th className="text-left px-4 py-2" > Estado </th>
          </tr>
        </thead>
        < tbody className="divide-y" >
          {enrichedData.length > 0 ? (
            enrichedData.map((item) =>
            (
              <tr key={item.id} >
                <td className="px-4 py-2" > {item.producto} </td>
                < td className="px-4 py-2" > {item.cantidad} </td>
                < td className="px-4 py-2" > {item.categoria} </td>
                < td className="px-4 py-2" > {item.precioUnitario} </td>
                < td className="px-4 py-2" > {item.precioTotal} </td>
                < td className="px-4 py-2" > {item.etiqueta} </td>
                < td className="px-4 py-2" >
                  <StatusBadge
                    status={item.estado === 'Disponible' ? 1 : 0}
                    text={item.estado}
                  />
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={7} className="text-center py-4">No items found</td>
            </tr>
          )
          }
        </tbody>
      </table>
    </div>
  );
};

export default InventoryTable;
