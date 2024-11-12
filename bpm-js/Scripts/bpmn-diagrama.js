const BpmnModeler = window.BpmnJS.default || window.BpmnJS;

const bpmnModeler = new BpmnModeler({
    container: '#bpmn-container'
});

function moverPaletaParaContainer() {
    const paletteContainer = document.querySelector('.editor-palette-container');
    const palette = document.querySelector('.djs-palette');
    if (palette && paletteContainer) {
        paletteContainer.appendChild(palette);
    }
}

async function carregarDiagrama() {
    const urlParams = new URLSearchParams(window.location.search);
    const fluxoId = urlParams.get('id');

    if (fluxoId) {
        try {
            const response = await fetch(`/Diagrama/GetDiagram?id=${fluxoId}`);
            const fluxo = await response.json();
            document.getElementById('nome-fluxo').value = fluxo.nome;
            await bpmnModeler.importXML(fluxo.xml_conteudo);
            console.log("Diagrama carregado com sucesso.");
        } catch (err) {
            console.error("Erro ao carregar o diagrama:", err);
        }
    } else {
        const diagramaVazio = `
        <?xml version="1.0" encoding="UTF-8"?>
        <bpmn:definitions xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:bpmn="http://www.omg.org/spec/BPMN/20100524/MODEL"
                          xmlns:bpmndi="http://www.omg.org/spec/BPMN/20100524/DI"
                          xmlns:dc="http://www.omg.org/spec/DD/20100524/DC"
                          xsi:schemaLocation="http://www.omg.org/spec/BPMN/20100524/MODEL BPMN20.xsd"
                          id="Definitions_1"
                          targetNamespace="http://bpmn.io/schema/bpmn">
          <bpmn:process id="Process_1" isExecutable="false">
            <bpmn:startEvent id="StartEvent_1"/>
          </bpmn:process>
          <bpmndi:BPMNDiagram id="BPMNDiagram_1">
            <bpmndi:BPMNPlane id="BPMNPlane_1" bpmnElement="Process_1">
              <bpmndi:BPMNShape id="_BPMNShape_StartEvent_2" bpmnElement="StartEvent_1">
                <dc:Bounds x="173" y="102" width="36" height="36"/>
              </bpmndi:BPMNShape>
            </bpmndi:BPMNPlane>
          </bpmndi:BPMNDiagram>
        </bpmn:definitions>
        `;
        await bpmnModeler.importXML(diagramaVazio);
        console.log("Diagrama vazio carregado com sucesso.");
    }
    moverPaletaParaContainer();
}

document.getElementById('salvar-diagrama').addEventListener('click', async () => {
    try {
        const { xml } = await bpmnModeler.saveXML({ format: true });
        const nomeDiagrama = document.getElementById('nome-fluxo').value;

        if (!nomeDiagrama) {
            alert("O nome do diagrama é obrigatório.");
            return;
        }

        const urlParams = new URLSearchParams(window.location.search);
        const fluxoId = urlParams.get('id');

        const response = await fetch("/Diagrama/SaveDiagram", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: fluxoId, nome: nomeDiagrama, diagram: xml })
        });

        const result = await response.json();
        alert(result.Status);
    } catch (err) {
        console.error("Erro ao salvar o diagrama:", err);
    }
});

document.getElementById('importar-diagrama').addEventListener('change', async (event) => {
    const arquivo = event.target.files[0];
    if (arquivo) {
        const leitor = new FileReader();
        leitor.onload = async function (event) {
            const xml = event.target.result;
            try {
                await bpmnModeler.importXML(xml);
                console.log("Diagrama importado com sucesso.");
                moverPaletaParaContainer();
            } catch (err) {
                console.error("Erro ao importar o diagrama:", err);
            }
        };
        leitor.readAsText(arquivo);
    }
});

carregarDiagrama();
