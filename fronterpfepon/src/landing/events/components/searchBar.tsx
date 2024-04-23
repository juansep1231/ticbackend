import React, { useState } from 'react';

const SearchBar = () => {
    const [searchQuery, setSearchQuery] = useState('');

    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(event.target.value);
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        // Aquí puedes realizar alguna acción con la búsqueda, como enviarla a una API o filtrar datos locales
        console.log('Búsqueda enviada:', searchQuery);
    };

    return (
        <div className="bg-gray-100 p-4 rounded-md shadow-md flex items-center justify-between">
            <form onSubmit={handleSubmit} className="flex items-center ">
                <input
                    type="text"
                    placeholder="Buscar..."
                    value={searchQuery}
                    onChange={handleSearchChange}
                    className="border border-gray-600 focus:outline-none focus:ring-1 focus:ring-blue-500 rounded-md px-4 py-2 w-max" // Cambiado el ancho a w-64
                />
                <button type="submit" className="bg-blue-500 text-white font-semibold px-4 py-2 ml-2 rounded-md hover:bg-blue-600">
                    Buscar
                </button>
                <button type="submit" className="bg-blue-500 text-white font-semibold px-4 py-2 ml-2 rounded-md hover:bg-blue-600">
                    Filtrar
                </button>
            </form>
            <div>
                <button type="submit" className="bg-blue-500 text-white font-semibold px-4 py-2 ml-2 rounded-md hover:bg-blue-600">
                    Vista
                </button>
                <button type="submit" className="bg-blue-500 text-white font-semibold px-4 py-2 ml-2 rounded-md hover:bg-blue-600">
                    Añadir
                </button>
            </div>
        </div>

    );
};

export default SearchBar;
