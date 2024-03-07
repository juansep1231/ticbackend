// PDFDownloadButton.tsx
import React from 'react';
import { useState } from 'react';
const PDFDownloadButton: React.FC = () => {

    const [showOptions, setShowOptions] = useState(false);
    return (
        <div className="relative inline-block text-left">
            <button onClick={() => setShowOptions(!showOptions)} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                Descargar
            </button>
            {showOptions && (
                <div className="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5">
                    <div className="py-1" role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                        <button className="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100" role="menuitem">
                            PDF
                        </button>
                        <button className="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100" role="menuitem">
                            Excel
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default PDFDownloadButton;